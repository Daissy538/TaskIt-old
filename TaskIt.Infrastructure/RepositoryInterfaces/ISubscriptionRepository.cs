using TaskItApi.Entities;

namespace TaskIt.Domain.RepositoryInterfaces
{
    public interface ISubscriptionRepository : IRepositoryBase<Subscription>
    {
        /// <summary>
        /// Subscribe user to group
        /// </summary>
        /// <param name="groupID">the group</param>
        /// <param name="userID">the user</param>
        /// <returns>true if succesfull subscribed, false otherwise</returns>
        bool SubscribeUser(int groupID, int userID);
        
        /// <summary>
        /// Unsubscribe user for group
        /// </summary>
        /// <param name="groupID">The group</param>
        /// <param name="userID">The user</param>
        /// <returns>true if succesfull unsubscribed</returns>
        bool UnSubscribeUser(int groupID, int userID);
    }
}
