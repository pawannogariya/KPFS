﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KPFS.Data.Entities
{
    public class User : IdentityUser<int>
    {
        [StringLength(100)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(100)]
        [Required]
        public string LastName { get; set; }

        [Required]
        public bool IsActive { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } 
    }
}
