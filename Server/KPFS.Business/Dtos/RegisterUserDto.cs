﻿using System.ComponentModel.DataAnnotations;

namespace KPFS.Business.Models
{
    public class RegisterUserDto
    {
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string? LastName { get; set; }
    }
}
