using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using TaskItApi.Dtos;
using TaskItApi.Entities;
using TaskItApi.Exceptions;
using TaskItApi.Handlers.Interfaces;
using TaskItApi.Models.Interfaces;
using TaskItApi.Services.Interfaces;

namespace TaskItApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<IAuthenticationService> logger, ITokenHandler tokenHandler)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _tokenHandler = tokenHandler;
        }

        public TokenDto AuthenicateUser(UserInComingDto userIncomingData)
        {
           User user = _unitOfWork.UserRepository.GetUser(userIncomingData.Email);

            bool userExist = user != default(User);
            bool passwordVerified = VerifyPasswordHash(userIncomingData.Password, user.PasswordHash, user.PasswordSalt);

            if (!userExist || !passwordVerified)
            {
                _logger.LogTrace($"A user with email {userIncomingData.Email} tride to login but failed.");
                throw new InvalidInputException("Email and/or password is incorrect");
            }

            string token = _tokenHandler.CreateAuthenticationToken(user);

            TokenDto tokenDto = new TokenDto();
            tokenDto.Token = token;

            return tokenDto;
        }

        public User RegisterUser(UserInComingDto userInComingDto)
        {
            CreatePasswordHash(userInComingDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User newUser = _mapper.Map<User>(userInComingDto);
            newUser.PasswordHash = passwordHash;
            newUser.PasswordSalt = passwordSalt;

            try
            {
                _unitOfWork.UserRepository.AddUser(newUser);
                _unitOfWork.SaveChanges();
                _logger.LogInformation($"User {newUser.Name} with email: {newUser.Email} is registered|");
                return newUser;
            }catch(Exception ex)
            {
                _logger.LogError($"Could not register user {newUser.Name} with email: {newUser.Email}");
                throw ex;
            }     
        }

        public bool UserExist(UserInComingDto userInComingDto)
        {
            User user = _unitOfWork.UserRepository.GetUser(userInComingDto.Email);
            return user != default(User);

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
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
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
                byte[] incommingPasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                
                return incommingPasswordHash.SequenceEqual(passwordHash);
            }
        }
       
    }
}
