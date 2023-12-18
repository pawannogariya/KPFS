﻿namespace KPFS.Business.Models
{
    public class UserDto : DtoBase
    {
        public string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public string Role { get; set; }

        public bool EmailConfirmed { get; set; }
    }
}
