﻿using System.ComponentModel.DataAnnotations;

namespace TaskIt.Core.Dtos
{
    public class AuthenticateDto
    {
        public string Email { get; set; }
 
        public string Password { get; set; }
    }
}
