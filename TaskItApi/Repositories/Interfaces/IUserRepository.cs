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
    }
}
