using KPFS.Business.Dtos;
using KPFS.Business.Models;
using KPFS.Data.Entities;

namespace KPFS.Business.Services.Interfaces
{
    public interface IMasterDataService
    {
        public Task<IEnumerable<FundHouseDto>> GetAllFundHousesAsync();
        public Task<IEnumerable<FundDto>> GetFundsAsync(int? fundHouseId);
        public Task<IEnumerable<BankAccountDto>> GetBankAccountsAsync(int? fundId);
        public Task<IEnumerable<FundManagerDto>> GetFundManagersAsync(int fundId);
        public Task<bool> DoesFundHouseExistAsync(string fundHouse);
        public Task AddOrUpdateFundHouseAsync(FundHouseDto fundHouse, User user);
        public Task<IEnumerable<RoleDto>> GetAllRolesAsync();
        public Task<bool> DoesFundExistAsync(string fundName, int fundId);
        public Task AddOrUpdateFundAsync(FundDto fund, User user);
    }
}
