using KPFS.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KPFS.Data.Repositories.Base
{
    public abstract class MasterEntityRepositoryBase<TEntity, TId> : EntityRepositoryBase<TEntity, TId> where TEntity : MasterEntityBase<TId>
    {
        protected MasterEntityRepositoryBase(KpfsDbContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        public async Task<TEntity> GetAsync(TId id)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(e => e.IsActive && Equals(e.Id, id));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().Where(e => e.IsActive).ToListAsync();
        }

        public async Task<bool> ExistsAsync(TId id)
        {
            return await Context.Set<TEntity>().AnyAsync(e => e.IsActive && Equals(e.Id, id));
        }
    }
}
