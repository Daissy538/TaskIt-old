using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskItApi.Dtos
{
    /// <summary>
    /// User information that can be returned to the frontend.
    /// </summary>
    public class UserOutGoingDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
