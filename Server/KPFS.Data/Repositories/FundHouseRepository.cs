using KPFS.Data.Entities;
using KPFS.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace KPFS.Data.Repositories
{
    public class FundHouseRepository : EntityRepositoryBase<FundHouse, int>
    {
        public FundHouseRepository(KpfsDbContext context) : base(context)
        {
        }

        public async Task<bool> DoesFundHouseExistsAsync(string fundHouse)
        {
            return await this.Context.FundHouses.AnyAsync(x => x.ShortName == fundHouse);
        }
    }
}
