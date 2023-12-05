using KPFS.Business.Dtos;

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
