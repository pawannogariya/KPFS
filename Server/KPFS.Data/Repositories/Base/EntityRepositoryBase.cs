using KPFS.Data.Entities;
using KPFS.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace KPFS.Data.Repositories.Base
{
    public abstract class EntityRepositoryBase<TEntity, TId> : RepositoryBase where TEntity : EntityBase<TId>
    {
        protected EntityRepositoryBase(KpfsDbContext context) : base(context)
        {
        }

        public async Task<TEntity> GetAsync(TId id)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(e => !e.IsDeleted && Equals(e.Id, id));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>().Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<bool> ExistsAsync(TId id)
        {
            return await Context.Set<TEntity>().AnyAsync(e => !e.IsDeleted && Equals(e.Id, id));
        }

        public async Task CreateAsync(TEntity entity, User user)
        {
            if (entity is null)
            {
                return;
            }

            await CreateAsync(new[] { entity }, user);
        }

        public async Task CreateAsync(IEnumerable<TEntity> entities, User user)
        {
            if (entities is null)
            {
                return;
            }

            foreach (var entity in entities)
            {
                entity.MarkAsCreatedBy(user);
                Context.Add(entity);
            }

            await Context.SaveChangesAsync();
            Context.ChangeTracker.Clear();
        }

        public async Task UpdateAsync(TEntity entity, User user)
        {
            if (entity is null)
            {
                return;
            }

            await UpdateAsync(new[] { entity }, user);
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities, User user)
        {
            if (entities is null)
            {
                return;
            }

            foreach (var entity in entities)
            {
                entity.MarkAsUpdatedBy(user);
                Context.Entry(entity).State = EntityState.Modified;

                Context.Entry(entity).Property(e => e.IsDeleted).IsModified = false;
                Context.Entry(entity).Property(e => e.DeletedBy).IsModified = false;
                Context.Entry(entity).Property(e => e.DeletedOn).IsModified = false;
                Context.Entry(entity).Property(e => e.CreatedOn).IsModified = false;
                Context.Entry(entity).Property(e => e.CreatedBy).IsModified = false;
            }

            await Context.SaveChangesAsync();
        }


        public async Task DeleteAsync(TEntity entity, User user)
        {
            if (entity is null)
            {
                return;
            }

            await DeleteAsync(new[] { entity }, user);
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities, User user)
        {
            if (entities is null)
            {
                return;
            }

            foreach (var entity in entities)
            {
                entity.MarkAsDeleted(user);

                Context.Attach(entity);
                Context.Entry(entity).State = EntityState.Modified;

                Context.Entry(entity).Property(e => e.IsDeleted).IsModified = true;
                Context.Entry(entity).Property(e => e.DeletedBy).IsModified = true;
                Context.Entry(entity).Property(e => e.DeletedOn).IsModified = true;

                Context.Entry(entity).Property(e => e.CreatedOn).IsModified = false;
                Context.Entry(entity).Property(e => e.CreatedBy).IsModified = false;
                Context.Entry(entity).Property(e => e.UpdatedBy).IsModified = false;
                Context.Entry(entity).Property(e => e.UpdatedOn).IsModified = false;
            }

            await Context.SaveChangesAsync();
        }

        public async Task CreateOrUpdateAsync(TEntity entity, User user)
        {
            if (entity.IsNew)
            {
                await CreateAsync(entity, user);
            }
            else
            {
                await UpdateAsync(entity, user);
            }
        }
    }
}
