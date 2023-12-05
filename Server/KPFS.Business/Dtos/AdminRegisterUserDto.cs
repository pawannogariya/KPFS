using System.ComponentModel.DataAnnotations;

namespace KPFS.Business.Models
{
    public class AdminRegisterUserDto : RegisterUserDto
    {
        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }
    }
}
