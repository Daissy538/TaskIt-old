using System;
using TaskItApi.Dtos;

namespace TaskItApi.Services.NewFolder
{
    /// <summary>
    /// Interface for authenication
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Authenticate User based on Incomingdata.
        /// On succes it will return a JWT string.
        /// </summary>
        /// <param name="userIncomingData">The incoming user data</param>
        /// <returns></returns>
        string AuthenicateUser(UserInComingDto userIncomingData);

        /// <summary>
        /// Register user based on incomingdata
        /// </summary>
        /// <param name="userInComingDto">The new user data</param>
        void RegisterUser(UserInComingDto userInComingDto);
    }
}
