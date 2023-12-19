using KPFS.Data.Entities;
using KPFS.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KPFS.Data.Repositories
{
    public class RoleRepository : RepositoryBase
    {
        public RoleRepository(KpfsDbContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync()
        {
            return await this.Context.Roles.OrderBy(r => r.HierarchyOrder).ToListAsync();
        }
    }
}
