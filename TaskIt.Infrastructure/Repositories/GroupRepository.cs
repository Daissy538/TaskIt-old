using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using TaskIt.Domain.RepositoryInterfaces;
using TaskItApi.Entities;
using TaskItApi.Models;

namespace TaskItApi.Repositories
{
    public class GroupRepository : RepositoryBase<Group>, IGroupRepository
    {

        public GroupRepository(TaskItDbContext taskItDbContext)
            : base(taskItDbContext)
        {
        }

        public async Task<List<Group>> FindAllGroupOfUserAsync(int userId)
        {
            List<Group> groups = await FindByCondition(g => g.Members.Where(
                                              m => m.User.ID == userId)                                       
                                              .Any())
                                        .Include(g => g.Icon)
                                        .Include(g => g.Color)
                                        .ToListAsync();
            return groups;
        }

        public async Task<Group?> FindGroupOfUserAsync(int groupId, int userId)
        {
            Group group = await FindByCondition(g => g.ID == groupId &&
                                               g.Members.Where(m => m.User.ID == userId)
                                               .Any())
                                          .Include(g => g.Icon)
                                          .Include(g => g.Color)
                                          .Include(g => g.Members)
                                            .ThenInclude(m => m.User)
                                          .FirstOrDefaultAsync();

            return group;
        }
    }
}
