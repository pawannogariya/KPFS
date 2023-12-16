using KPFS.Data.Entities;
using KPFS.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace KPFS.Data.Repositories
{
    public class RoleRepository : RepositoryBase
    {
        public RoleRepository(KpfsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await this.Context.Roles.OrderBy(r => r.HierarchyOrder).ToListAsync();
        }
    }
}
