namespace KPFS.Data.Repositories.Base
{
    public abstract class RepositoryBase
    {
        protected KpfsDbContext Context { get; }
        protected RepositoryBase(KpfsDbContext context)
        {
            Context = context;
        }
    }
}
