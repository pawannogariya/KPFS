using KPFS.Data.Entities.Base;
using Microsoft.Extensions.Configuration;

namespace KPFS.Data.Repositories.Base
{
    public abstract class EntityRepositoryBase<TEntity, TId> : RepositoryBase where TEntity : EntityBase<TId>
    {
        protected EntityRepositoryBase(KpfsDbContext context, IConfiguration configuration) : base(context, configuration)
        {
        }
    }
}
