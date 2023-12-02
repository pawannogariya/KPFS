using KPFS.Data.Entities;

namespace KPFS.Data.Repositories
{
    public class FundRepository : RepositoryBase<Fund, int>
    {
        public FundRepository(KpfsDbContext context) : base(context)
        {
        }
    }
}
