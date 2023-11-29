using KPFS.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace KPFS.Data.Repositories
{
    public class BankAccountRepository : RepositoryBase<BankAccount, int>
    {
        public BankAccountRepository(KpfsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BankAccount>> GetFundBankAccountsAsync(int fundId)
        {
            return await this.Context.BankAccounts.Include(x => x.Fund).Where(x => !x.IsDeleted && x.FundId == fundId).ToListAsync();
        }
    }
}
