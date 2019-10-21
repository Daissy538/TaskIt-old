using System;

namespace TaskItApi.Dtos
{
    /// <summary>
    /// The subscription of a user to a group
    /// </summary>
    public class SubscriptionOutgoingDto
    {
        /// <summary>
        /// The id of the subscribed user
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// The name of the subscribed user
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// The group id where the user is subscribed to
        /// </summary>
        public int GroupID { get; set; }
        /// <summary>
        /// The group name where the user is subscribed to
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// The data of the subscription
        /// </summary>
        public DateTime DateOfSubscription { get; set; }
    }
}
