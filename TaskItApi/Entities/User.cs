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
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
