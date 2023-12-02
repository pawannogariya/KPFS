using KPFS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KPFS.Data.Repositories
{
    public class FundHouseRepository : RepositoryBase<FundHouse, int>
    {
        public FundHouseRepository(KpfsDbContext context) : base(context)
        {
        }
    }
}
