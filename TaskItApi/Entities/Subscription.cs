using System;
using System.ComponentModel.DataAnnotations;

namespace TaskItApi.Entities
{
    /// <summary>
    /// A user can subscribed to a group.
    /// </summary>
    public class Subscription
    {
        public int ID { get; set; }
        [Required]
        public Group Group { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public DateTime DateOfSubscription { get; set; }
    }
}
