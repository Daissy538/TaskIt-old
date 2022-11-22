using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TaskIt.Domain.RepositoryInterfaces;
using TaskItApi.Entities;
using TaskItApi.Models;

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
            Create(user);
        }

        public async Task<User?> GetUserAsync(string email)
        {
            email = email.ToLower();

            var user = await FindByCondition(u => u.Email == email)
                        .Include(u => u.Subscriptions)
                        .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserAsync(int id)
        {
            var user = await FindByCondition(u => u.ID == id)
                        .Include(u => u.Subscriptions)
                        .FirstOrDefaultAsync();

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
