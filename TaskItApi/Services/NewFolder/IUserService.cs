﻿using TaskItApi.Dtos;

namespace TaskItApi.Services.NewFolder
{
    /// <summary>
    /// Interface for managing the users
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Delete user by userId
        /// </summary>
        /// <param name="Id">the user Id</param>
        /// <returns></returns>
        bool DeleteUser(int Id);
        /// <summary>
        /// Update the user with the given user Id
        /// </summary>
        /// <param name="Id">the user to be updated</param>
        /// <param name="changedUserInComingData">The changed data</param>
        void UpdateUser(int Id, UserInComingDto changedUserInComingData);
        /// <summary>
        /// Validate the data and add the user to the database
        /// </summary>
        /// <param name="userInComingData">The incoming user data of the new user</param>
        void CreateUser(UserInComingDto userInComingData);
    }
}
