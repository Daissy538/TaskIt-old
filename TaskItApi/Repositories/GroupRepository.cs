using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Group> FindAllGroupOfUser(int userId)
        {
            IEnumerable<Group> groups = this.FindByCondition(g => g.Members.Where(
                                      m => m.User.ID.Equals(userId))                                       
                                      .Any());
            return groups;
        }

        public Group FindGroupOfUser(int groupId, int userId)
        {
            Group group = FindByCondition(g => g.ID.Equals(groupId) &&
                                               g.Members.Where(m => m.User.ID.Equals(userId)).Any())
                                          .Include(g => g.Members)
                                          .FirstOrDefault();

            return group;
        }

    }
}
