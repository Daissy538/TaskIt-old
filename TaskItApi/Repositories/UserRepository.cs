using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskItApi.Entities;
using TaskItApi.Exceptions;
using TaskItApi.Models;
using TaskItApi.Repositories.Interfaces;

namespace TaskItApi.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly ILogger _logger;

        public UserRepository(TaskItDbContext taskItDbContext, ILogger<IUserRepository> logger)
            :base(taskItDbContext)
        {
            _logger = logger;
        }

        public void AddUser(User user)
        {
            user.Email = user.Email.ToLower();

            IEnumerable<User> ExistingUsersWithSameEmail = FindByCondition(u => u.Email == user.Email)
                                                           .ToList();

            if (ExistingUsersWithSameEmail.Any())
            {
                _logger.LogError($"Could not register user with {user.Email}. Email already exist");
                throw new InvalidInputException($"User with email {user.Email} already exist");
            }

            try
            {
                Create(user);                                
            }catch(Exception exception)
            {
                _logger.LogError($"Register user {user.Name} with email: {user.Email}", exception);
                throw new SystemException($"Could not register user {user.Email}");
            }
            
        }

        public User GetUser(string email)
        {
            email = email.ToLower();

            User user = FindByCondition(u => u.Email == email)
                        .Include(u => u.Subscriptions)        
                        .FirstOrDefault();

            return user;
        }

        public User GetUser(int id)
        {
            User user = FindByCondition(u => u.ID == id)
                        .Include(u => u.Subscriptions)
                        .FirstOrDefault();

            return user;
        }

        public bool ContainceUser(int id)
        {
            bool result = FindByCondition(u => u.ID == id).Any();
            return result;
        }

        public bool ContainceUser(string email)
        {
            bool result = FindByCondition(u => u.Email == email).Any();
            return result;
        }
    }
}
