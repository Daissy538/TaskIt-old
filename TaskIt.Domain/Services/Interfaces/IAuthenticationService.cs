using TaskIt.Core.Dtos;
using TaskItApi.Entities;

namespace TaskItApi.Services.Interfaces
{
    /// <summary>
    /// Interface for authenication
    /// </summary>
    public interface IAuthenticationService
    {
        public Task<string> AuthenticateUser(AuthenticateDto userIncomingData);
        public Task<User> RegisterUserAsync(string mockName, string mockEmail, string mockPassword);

    }
}
