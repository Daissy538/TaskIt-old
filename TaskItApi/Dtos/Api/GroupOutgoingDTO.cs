using System.Collections.Generic;

namespace TaskItApi.Dtos
{
    /// <summary>
    /// The outgoing group data
    /// </summary>
    public class GroupOutgoingDTO
    {
        /// <summary>
        /// The unique database id
        /// </summary>
        public int ID { get; set; }        
        /// <summary>
        /// The name of the group
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The description of the group
        /// </summary>
        public string Description { get; set; }   
        /// <summary>
        /// The icon value
        /// </summary>
        public string IconValue { get; set; }
        /// <summary>
        /// The icon name that the user will see
        /// </summary>
        public string IconName { get; set; }
        /// <summary>
        /// The color value
        /// </summary>
        public string ColorValue { get; set; }
        /// <summary>
        /// The color name that the user will see
        /// </summary>
        public string ColorName { get; set; }
        /// <summary>
        /// The member that are subscribed on the group
        /// </summary>
        public IEnumerable<SubscriptionOutgoingDto> Members;
    }
}

