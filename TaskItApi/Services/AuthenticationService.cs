using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using TaskItApi.Dtos;
using TaskItApi.Entities;
using TaskItApi.Models.Interfaces;
using TaskItApi.Services.NewFolder;

namespace TaskItApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger _logger;

        public AuthenticationService(IMapper mapper, IUnitOfWork unitOfWork, ILogger<IAuthenticationService> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public string AuthenicateUser(UserInComingDto userIncomingData)
        {
            throw new NotImplementedException();
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
    }
}
