using KPFS.Data.Entities;
using KPFS.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace KPFS.Data.Repositories
{
    public class BankAccountRepository : EntityRepositoryBase<BankAccount, int>
    {
        public BankAccountRepository(KpfsDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<BankAccount>> GetBankAccountsAsync(int? fundId)
        {
            return await this.Context.BankAccounts.Include(x => x.Fund).Where(x => !fundId.HasValue || (!x.IsDeleted && x.FundId == fundId)).ToListAsync();
        }
    }
}
