using System.ComponentModel.DataAnnotations;

namespace KPFS.Business.Models
{
    public class AddUserDto : RegisterUserDto
    {
        [Required(ErrorMessage = "Role is required")]
        public string? Role { get; set; }
    }
}
