using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using TaskItApi.Dtos;
using TaskItApi.Entities;
using TaskItApi.Exceptions;
using TaskItApi.Extentions;
using TaskItApi.Models.Interfaces;
using TaskItApi.Services.NewFolder;

namespace TaskItApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public AuthenticationService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<IAuthenticationService> logger, IConfiguration config)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _configuration = config;
        }

        public TokenDto AuthenicateUser(UserInComingDto userIncomingData)
        {
            User user = _unitOfWork.UserRepository.GetUser(userIncomingData.Email);

            bool userExist = !user.Equals(default(User));
            bool passwordVerified = VerifyPasswordHash(userIncomingData.Password, user.PasswordHash, user.PasswordSalt);

            if (!userExist && !passwordVerified)
            {
                _logger.LogTrace($"A user with email {userIncomingData.Email} tride to login but failed.");
                throw new InvalidInputException("Email and/or password is incorrect");
            }

            string token = CreateToken(user);

            TokenDto tokenDto = new TokenDto();
            tokenDto.Token = token;

            return tokenDto;
        }

        public void RegisterUser(UserInComingDto userInComingDto)
        {
            CreatePasswordHash(userInComingDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User newUser = this._mapper.Map<User>(userInComingDto);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            _unitOfWork.UserRepository.AddUser(newUser);
            _unitOfWork.SaveChanges();
            _logger.LogInformation($"User {newUser.Name} with email: {newUser.Email} is added to the databse");                     
        }

        public bool UserExist(UserInComingDto userInComingDto)
        {
            User user = _unitOfWork.UserRepository.GetUser(userInComingDto.Email);
            return !user.Equals(default(User));

        }

        /// <summary>
        /// Hash the user password based on the password and a salt.
        /// </summary>
        /// <param name="password">the password of the user</param>
        /// <param name="passwordHash">the hashed password</param>
        /// <param name="passwordSalt">the password salt</param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }            
        }

        /// <summary>
        /// Verify the password of the incoming user with the user from the database.
        /// </summary>
        /// <param name="password">incoming password</param>
        /// <param name="passwordHash">hashed password in database</param>
        /// <param name="passwordSalt">satl in database</param>
        /// <returns>Return true if it is a match, return false if it isn't</returns>
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                byte[] incommingPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return incommingPasswordHash.SequenceEqual(passwordHash);
            }
        }
        
        /// <summary>
        /// Create authentication token voor given user
        /// </summary>
        /// <param name="user">The user that want to login</param>
        /// <returns>The created token</returns>
        private string CreateToken(User user)
        {
            Claim[] claims = ClaimExtension.GenerateUserClaims(user.ID, user.Name);
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:AppSecret"]));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims, "jwt"),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
