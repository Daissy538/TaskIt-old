using System.Collections.Generic;
using TaskItApi.Entities;

namespace TaskItApi.Repositories.Interfaces
{
    public interface IGroupRepository: IRepositoryBase<Group>
    {
        /// <summary>
        /// Find all the groups where the user is subscribed on
        /// </summary>
        /// <param name="userID">The id of the user</param>
        /// <returns>The subscribed groups</returns>
        IEnumerable<Group> FindAllGroupOfUser(int userID);

        /// <summary>
        /// Find a group by id for where the active user is subscribed on
        /// </summary>
        /// <param name="groupID">The group id</param>
        /// <param name="userID">The active user id</param>
        /// <returns>Returns the group if the user is subscribed on it</returns>
        Group FindGroupOfUser(int groupID, int userID);

        /// <summary>
        /// Check if user is subscribed on the group
        /// </summary>
        /// <param name="groupID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        bool IsSubscribed(int groupID, int userID);      
    }
}
