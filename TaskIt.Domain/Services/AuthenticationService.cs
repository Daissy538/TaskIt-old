using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Text;
using TaskIt.Core.Dtos;
using TaskItApi.Entities;
using TaskItApi.Handlers.Interfaces;
using TaskItApi.Models.Interfaces;
using TaskItApi.Services.Interfaces;
using Task = System.Threading.Tasks.Task;

namespace TaskItApi.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationService(IUnitOfWork unitOfWork, ITokenHandler tokenHandler)
        {
            this._unitOfWork = unitOfWork;
            this._tokenHandler = tokenHandler;
        }

        public async Task<string> AuthenticateUser(AuthenticateDto authenticate)
        {
            User? user = await _unitOfWork.UserRepository.GetUserAsync(authenticate.Email);

            if (user == null)
            {
                throw new NullReferenceException("User doesn't exist");
            }

            bool userExist = user != default(User);
            bool passwordVerified = VerifyPasswordHash(authenticate.Password, user.PasswordHash, user.PasswordSalt);

            if (!userExist || !passwordVerified)
            {
                throw new AuthenticationException("Email and/or password is incorrect");
            }

            string token = _tokenHandler.CreateAuthenticationToken(user);

            return token;
        }

        public async Task<User> RegisterUserAsync(string name, string email, string password)
        {
            email = email.ToLower();

            if (this._unitOfWork.UserRepository.ContainceUser(email))
            {
                throw new ValidationException($"User with email {email} already exist");
            }

            var user = User.Create(name, email, password);

            _unitOfWork.UserRepository.Update(user);
           await _unitOfWork.SaveChangesAsync();

            return user;
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
