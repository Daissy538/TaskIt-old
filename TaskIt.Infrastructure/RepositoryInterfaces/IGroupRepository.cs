using System.Collections.Generic;
using TaskItApi.Entities;

namespace TaskIt.Domain.RepositoryInterfaces
{
    public interface IGroupRepository : IRepositoryBase<Group>
    {
        /// <summary>
        /// Find all the groups where the user is subscribed on
        /// </summary>
        /// <param name="userId">The id of the user</param>
        /// <returns>The subscribed groups</returns>
        Task<List<Group>> FindAllGroupOfUserAsync(int userId);
        /// <summary>
        /// Find a group by id for where the active user is subscribed on
        /// </summary>
        /// <param name="groupId">The group id</param>
        /// <param name="userId">The active user id</param>
        /// <returns>Returns the group if the user is subscribed on it</returns>
        Task<Group?> FindGroupOfUserAsync(int groupId, int userId);

    }
}
