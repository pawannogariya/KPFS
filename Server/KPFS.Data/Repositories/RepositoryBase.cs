using KPFS.Data.Entities;
using KPFS.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace KPFS.Data.Repositories
{
    public abstract class RepositoryBase<TEntity, TId> where TEntity : EntityBase<TId>
    {
        protected KpfsDbContext Context { get; }
        protected RepositoryBase(KpfsDbContext context)
        {
            this.Context = context;
        }

        public async Task<TEntity> GetAsync(TId id)
        {
            return await Context.Set<TEntity>().FirstOrDefaultAsync(e => !e.IsDeleted && Equals(e.Id, id));
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await this.Context.Set<TEntity>().Where(e => !e.IsDeleted).ToListAsync();
        }

        public async Task<bool> ExistsAsync(TId id)
        {
            return await this.Context.Set<TEntity>().AnyAsync(e => !e.IsDeleted && Equals(e.Id, id));
        }

        public async Task CreateAsync(TEntity entity, User user=null)
        {
            if (entity is null)
            {
                return;
            }

            await this.CreateAsync(new[] { entity }, user);
        }

        public async Task CreateAsync(IEnumerable<TEntity> entities, User user = null)
        {
            if (entities is null)
            {
                return;
            }

            foreach (var entity in entities)
            {
                entity.MarkAsCreatedBy(user);
                this.Context.Add(entity);
            }

            await this.Context.SaveChangesAsync();
            this.Context.ChangeTracker.Clear();
        }

        public async Task UpdateAsync(TEntity entity, User user = null)
        {
            if (entity is null)
            {
                return;
            }

            await this.UpdateAsync(new[] { entity }, user);
        }

        public async Task UpdateAsync(IEnumerable<TEntity> entities, User user = null)
        {
            if (entities is null)
            {
                return;
            }

            foreach (var entity in entities)
            {
                entity.MarkAsUpdatedBy(user);
                this.Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

                this.Context.Entry(entity).Property(e => e.IsDeleted).IsModified = false;
                this.Context.Entry(entity).Property(e => e.CreatedOn).IsModified = false;
                this.Context.Entry(entity).Property(e => e.CreatedBy).IsModified = false;
            }

            await this.Context.SaveChangesAsync();
        }


        public async Task DeleteAsync(TEntity entity, User user = null)
        {
            if (entity is null)
            {
                return;
            }

            await this.DeleteAsync(new[] { entity }, user);
        }

        public async Task DeleteAsync(IEnumerable<TEntity> entities, User user = null)
        {
            if (entities is null)
            {
                return;
            }

            foreach (var entity in entities)
            {
                entity.MarkAsDeleted(user);
                this.Context.Attach(entity);
                this.Context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                this.Context.Entry(entity).Property(e => e.IsDeleted).IsModified = true;
            }

            await this.Context.SaveChangesAsync();
        }

        public async Task CreateOrUpdateAsync(TEntity entity)
        {
            if(entity.IsNew)
            {
                await this.CreateAsync(entity);
            }
            else
            {
                await this.UpdateAsync(entity);
            }
        }
    }
}
