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

        public GroupRepository(TaskItDbContext taskItDbContext, ILogger<IGroupRepository> logger)
            : base(taskItDbContext)
        {
            _logger = logger;
        }

        public IEnumerable<Group> FindAllGroupOfUser(int userId)
        {
            IEnumerable<Group> groups = FindByCondition(g => g.Members.Where(
                                              m => m.User.ID == userId)                                       
                                              .Any())
                                        .Include(g => g.Icon)
                                        .Include(g => g.Color)
                                        .AsEnumerable();
            return groups;
        }

        public Group FindGroupOfUser(int groupId, int userId)
        {
            Group group = FindByCondition(g => g.ID == groupId &&
                                               g.Members.Where(m => m.User.ID == userId)
                                               .Any())
                                          .Include(g => g.Icon)
                                          .Include(g => g.Color)
                                          .Include(g => g.Members)
                                            .ThenInclude(m => m.User)
                                          .FirstOrDefault();

            return group;
        }
    }
}
