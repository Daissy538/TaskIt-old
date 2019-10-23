using System;
using TaskItApi.Dtos;
using TaskItApi.Services.Interfaces;

namespace TaskItApi.Services
{
    /// <summary>
    /// Managing users and authenication
    /// </summary>
    public class UserService : IUserService
    {
        public bool DeleteUser(int Id)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(int Id, UserInComingDto changedUserInComingData)
        {
            throw new NotImplementedException();
        }
    }
}
