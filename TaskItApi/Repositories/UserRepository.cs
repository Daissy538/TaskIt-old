﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using TaskItApi.Entities;
using TaskItApi.Models;
using TaskItApi.Repositories.Interfaces;

namespace TaskItApi.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly ILogger _logger;

        public UserRepository(TaskItDbContext taskItDbContext, ILogger<UserRepository> logger)
            :base(taskItDbContext)
        {
            _logger = logger;
        }

        public void AddUser(User user)
        {
            IEnumerable<User> ExistingUsersWithSameEmail = FindByCondition(u => string.Equals(u.Email, user.Email, StringComparison.OrdinalIgnoreCase));

            if (ExistingUsersWithSameEmail.Any())
            {
                _logger.LogError($"Could not register user with {user.Email}. Email already exist");
                throw new ArgumentException($"User with email {user.Email} already exist");
            }

            try
            {
                Create(user);                                
            }catch(SqlException exception)
            {
                _logger.LogError($"Register user {user.Name} with email: {user.Email}", exception);
                throw new SystemException($"Could not register user {user.Email}");
            }
            
        }
    }
}