using KPFS.Data.Entities;
using KPFS.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KPFS.Data.Repositories
{
    public class BankAccountRepository : EntityRepositoryBase<BankAccount, int>
    {
        public BankAccountRepository(KpfsDbContext context, IConfiguration configuration) : base(context, configuration)
        {
        }

        public async Task<IEnumerable<BankAccount>> GetBankAccountsAsync(int? fundId)
        {
            return await this.Context.BankAccounts.Include(x => x.Fund).Where(x => !fundId.HasValue || (!x.IsDeleted && x.FundId == fundId)).ToListAsync();
        }
    }
}
