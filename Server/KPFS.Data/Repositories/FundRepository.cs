using KPFS.Data.Entities;
using KPFS.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KPFS.Data.Repositories
{
    public class FundRepository : EditEntityRepositoryBase<Fund, int>
    {
        public FundRepository(KpfsDbContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        public async Task<bool> DoesFundExistsAsync(string fundName, int fundHouseId)
        {
            return await this.Context.Funds.AnyAsync(x => x.ShortName == fundName && x.FundHouseId == fundHouseId);
        }

        public async Task<IEnumerable<Fund>> GetFundsAsync(int? fundHouseId)
        {
            return await this.Context.Funds.Where(x => !fundHouseId.HasValue || (x.FundHouseId == fundHouseId && !x.IsDeleted)).ToListAsync();
        }
    }
}
