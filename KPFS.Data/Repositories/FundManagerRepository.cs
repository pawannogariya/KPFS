using KPFS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KPFS.Data.Repositories
{
    public class FundManagerRepository : RepositoryBase<FundManager, int>
    {
        public FundManagerRepository(KpfsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FundManager>> GetFundManagersAsync(int fundId)
        {
            return await this.Context.FundManagers.Include(x=>x.Fund)
                .Where(x => !x.IsDeleted && x.FundId == fundId).ToListAsync();
        }
    }
}
