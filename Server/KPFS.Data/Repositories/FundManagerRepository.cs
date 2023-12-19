using KPFS.Data.Entities;
using KPFS.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KPFS.Data.Repositories
{
    public class FundManagerRepository : EntityRepositoryBase<FundManager, int>
    {
        public FundManagerRepository(KpfsDbContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        public async Task<IEnumerable<FundManager>> GetFundManagersAsync(int fundId)
        {
            return await this.Context.FundManagers.Include(x => x.Fund)
                .Where(x => !x.IsDeleted && x.FundId == fundId).ToListAsync();
        }
    }
}
