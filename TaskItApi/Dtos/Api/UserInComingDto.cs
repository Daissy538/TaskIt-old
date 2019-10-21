using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItApi.Dtos
{
    /// <summary>
    /// Used only for incoming calls at the controller
    /// </summary>
    public class UserInComingDto
    {
        /// <summary>
        /// The unique email of the user
        /// </summary>
        [Required]
        public string Email { get; set; }
        /// <summary>
        /// The nickname that is visable on TaskIt
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The password that is used
        /// </summary>
        [Required]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "You must specify password between 8 and 20 characters")]
        public string Password { get; set; }
    }
}
