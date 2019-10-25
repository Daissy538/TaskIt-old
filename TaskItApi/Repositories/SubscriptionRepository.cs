using System;
using System.Linq;
using TaskItApi.Entities;
using TaskItApi.Exceptions;
using TaskItApi.Models;
using TaskItApi.Repositories.Interfaces;

namespace TaskItApi.Repositories
{
    public class SubscriptionRepository : RepositoryBase<Subscription>, ISubscriptionRepository
    {    

        public SubscriptionRepository(TaskItDbContext taskItDbContext)
            : base(taskItDbContext)
        {
        }

        /// <summary>
        /// Subscribe user to group
        /// </summary>
        /// <param name="groupID">the group</param>
        /// <param name="userID">the user</param>
        /// <returns>true if succesfull subscribed, false otherwise</returns>
        public bool SubscribeUser(int groupID, int userID)
        {
            bool hasAlreadySubscribed = FindByCondition(s => s.UserID == userID && s.GroupID == groupID).Any();

            if(hasAlreadySubscribed)
            {
                throw new InvalidInputException($"User {userID} is already subscribed to the group {groupID}");
            }

            Subscription subscription = new Subscription()
            {
                GroupID = groupID,
                UserID = userID,
                DateOfSubscription = DateTime.Now
            };

            Create(subscription);

            return true;
        }

        /// <summary>
        /// Unsubscribe user for group
        /// </summary>
        /// <param name="groupID">The group</param>
        /// <param name="userID">The user</param>
        /// <returns>true if succesfull unsubscribed</returns>
        public bool UnSubscribeUser(int groupID, int userID)
        {
            Subscription subscription = FindByCondition(s => s.UserID == userID && s.GroupID == groupID).FirstOrDefault();

            if (subscription == default(Subscription))
            {
                throw new InvalidInputException($"User {userID} is not subscribed for group {groupID}");
            }

            Delete(subscription);

            return true;
        }
    }
}
