using Microsoft.AspNetCore.Identity;

namespace KPFS.Data.Entities
{
    public class UserRole : IdentityUserRole<string>
    {
        //[ForeignKey(nameof(UserId))]
        //public virtual User User { get; set; }

        //[ForeignKey(nameof(RoleId))]
        //public virtual Role Role { get; set; }
    }
}
