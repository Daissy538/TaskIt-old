using Microsoft.Extensions.Logging;
using TaskItApi.Entities;
using TaskItApi.Models;
using TaskItApi.Repositories.Interfaces;

namespace TaskItApi.Repositories
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {
        private readonly ILogger _logger;

        public GroupRepository(TaskItDbContext taskItDbContext, ILogger<GroupRepository> logger)
            : base(taskItDbContext)
        {
            _logger = logger;
        }


    }
}
