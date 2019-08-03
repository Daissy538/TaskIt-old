using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskItApi.Entities
{
    /// <summary>
    /// User of the TaskItApplication can register to multiple groups.
    /// </summary>
    public class User
    {
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Nickname { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
