using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskItApi.Entities;

namespace TaskItApi.Repositories.Interfaces
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        /// <summary>
        /// Add user to the database only when the email doesn't already exist
        /// </summary>
        /// <param name="user">The user to be added</param>
        void AddUser(User user);
        /// <summary>
        /// Get user based on email
        /// </summary>
        /// <param name="email">The given user</param>
        /// <returns>The user. Returns null if the user doesn't exist.</returns>
        User GetUser(string email);
        /// <summary>
        /// Get user bases on Id
        /// </summary>
        /// <param name="id">The id of the user</param>
        /// <returns>The user. Returns null if the user doesn't exist.</returns>
        User GetUser(int id);
        /// <summary>
        /// Check if user exist
        /// </summary>
        /// <param name="id">the user id</param>
        /// <returns>true if the user exist, false otherwise</returns>
        bool ContainceUser(int id);
        /// <summary>
        /// Check if user exist
        /// </summary>
        /// <param name="email">the user email</param>
        /// <returns>true if the user exist, false otherwise</returns>
        bool ContainceUser(string email);
    }
}
