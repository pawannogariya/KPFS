using KPFS.Business.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KPFS.Business.Services.Interfaces
{
    public interface IMasterDataService
    {
        public Task<IEnumerable<FundHouseDto>> GetAllFundHousesAsync();
        public Task<IEnumerable<FundDto>> GetAllFundsAsync();
        public Task<IEnumerable<BankAccountDto>> GetAllBankAccountsAsync();
        public Task<IEnumerable<BankAccountDto>> GetFundBankAccountsAsync(int fundId);
        public Task<IEnumerable<FundManagerDto>> GetFundManagersAsync(int fundId);
    }
}
