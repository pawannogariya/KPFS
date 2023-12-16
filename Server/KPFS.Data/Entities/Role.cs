using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KPFS.Data.Entities
{
    public class Role : IdentityRole<int>
    {
        [Required]
        [StringLength(100)] 
        public string DisplayName { get; set; }

        [Required]
        public int HierarchyOrder { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
