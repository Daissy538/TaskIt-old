
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
        /// <param name="newgroup">The group details</param>
        /// <param name="userId">The user that create the group</param>
        /// <returns>The current subscripted groups of the user</returns>
        IEnumerable<Group> Create(GroupIncomingDTO newgroup, int userId);

        /// <summary>
        /// Delete a group by group id
        /// </summary>
        /// <param name="groupId">The id of the group</param>
        /// <param name="userId">The user that delete the group</param>
        /// <returns>The current subscripted groups of the user</returns>
        IEnumerable<Group> Delete(int groupId, int userId);

        /// <summary>
        /// Get the all the groups where the user is subscribed on
        /// </summary>
        /// <param name="userId">The active user</param>
        /// <returns>The subscribed groups of the user</returns>
        IEnumerable<Group> GetGroups(int userId);

        /// <summary>
        /// Get group details based on the groupId. If the user is subscribed on
        /// </summary>
        /// <param name="groupId">The group id</param>
        /// <param name="userId">The active user</param>
        /// <returns>the group, null if the group doesn't exist or the user is not a subscriber</returns>
        Group GetGroup(int groupId, int userId);

        /// <summary>
        /// Update the selected groupd
        /// </summary>
        /// <param name="groupId">The group to be updated</param>
        /// <param name="newGroupData">the updated group data</param>
        /// <param name="userId">the user that requested the update</param>
        /// <returns></returns>
        Group Update(int groupId, GroupIncomingDTO newGroupData, int userId);

        /// <summary>
        /// Invite user to a group
        /// </summary>
        /// <param name="emailAddress">the email of the user to be invited</param>
        /// <param name="groupID">The group where the user is invited for</param>
        /// <param name="sendingUserID">The id of the user that is sending the invitation</param>
        /// <returns>true if the invitation is send, false otherwise</returns>
        bool InviteUserToGroup(int sendingUserID, string emailAddress, int groupID);
    }
}
