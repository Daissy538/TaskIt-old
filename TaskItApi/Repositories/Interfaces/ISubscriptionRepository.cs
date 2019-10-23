using TaskItApi.Entities;

namespace TaskItApi.Repositories.Interfaces
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
    }
}
