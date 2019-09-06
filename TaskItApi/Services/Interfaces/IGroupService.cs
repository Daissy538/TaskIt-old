
using System.Collections.Generic;
using TaskItApi.Dtos;
using TaskItApi.Entities;

namespace TaskItApi.Services.Interfaces
{
    /// <summary>
    /// Interface for managing groups
    /// </summary>
    public interface IGroupService
    {
        /// <summary>
        /// Create a group by an user
        /// </summary>
        /// <param name="groupDto">The group details</param>
        /// <param name="userName">The user that create the group</param>
        /// <returns>The current subscripted groups of the user</returns>
        IEnumerable<Group> Create(GroupDto groupDto, int userId);

        /// <summary>
        /// Delete a group by group id
        /// </summary>
        /// <param name="groupId">The id of the group</param>
        /// <param name="userName">The user that delete the group</param>
        /// <returns>The current subscripted groups of the user</returns>
        IEnumerable<Group> Delete(int groupId, string userName);
    }
}
