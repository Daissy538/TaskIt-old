using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
