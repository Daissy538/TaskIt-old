using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskItApi.Entities
{
    /// <summary>
    /// A user can subscribed to a group.
    /// </summary>
    public class Subscription
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public int GroupID { get; set; }
        [Required]
        public Group Group { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public User User { get; set; }
        [Required]
        public DateTime DateOfSubscription { get; set; }
    }
}
