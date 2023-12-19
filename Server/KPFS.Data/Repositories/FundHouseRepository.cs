using KPFS.Data.Entities;
using KPFS.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KPFS.Data.Repositories
{
    public class FundHouseRepository : EditEntityRepositoryBase<FundHouse, int>
    {
        public FundHouseRepository(KpfsDbContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        public async Task<bool> DoesFundHouseExistsAsync(string fundHouse)
        {
            return await this.Context.FundHouses.AnyAsync(x => x.ShortName == fundHouse);
        }
    }
}
