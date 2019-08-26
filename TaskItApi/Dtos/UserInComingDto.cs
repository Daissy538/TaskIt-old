using System;
using System.Collections.Generic;
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
        public string Email { get; set; }
        /// <summary>
        /// The nickname that is visable on TaskIt
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The password that is used
        /// </summary>
        public string Password { get; set; }
    }
}
