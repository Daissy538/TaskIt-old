using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.RegularExpressions;

namespace TaskItApi.Entities
{
    /// <summary>
    /// User of the TaskItApplication can register to multiple groups.
    /// </summary>
    public class User
    {
        [Key]
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

        public static User Create(string name, string email, string password)
        {
            if(!Regex.IsMatch(email, "^[a-zA-Z0-9]+(?:\\.[a-zA-Z0-9]+)*@[a-zA-Z0-9]+(?:\\.[a-zA-Z0-9]+)*$"))
            {
                throw new ValidationException("Incorrect email format");
            }

            var user = new User();

            user.Email = email;
            user.Name = name;

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                user.PasswordSalt = hmac.Key;
                user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

            return user;
        }
    }
}
