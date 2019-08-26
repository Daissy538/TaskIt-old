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
    }
}
