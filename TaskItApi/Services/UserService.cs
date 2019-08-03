using System;
using TaskItApi.Dtos;
using TaskItApi.Services.NewFolder;

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

        public void CreateUser(UserInComingDto userInComingData)
        {
            throw new NotImplementedException();
        }

        public void UpdateUser(int Id, UserInComingDto userInComingDto)
        {
            throw new NotImplementedException();
        }
    }
}
