using AutoMapper;
using KPFS.Business.Dtos;
using KPFS.Business.Models;
using KPFS.Business.Services.Interfaces;
using KPFS.Data.Entities;
using KPFS.Data.Repositories;

namespace KPFS.Business.Services.Implementations
{
    public class MasterDataService : IMasterDataService
    {
        private readonly FundRepository fundRepository;
        private readonly FundHouseRepository fundHouseRepository;
        private readonly BankAccountRepository bankAccountRepository;
        private readonly FundManagerRepository fundManagerRepository;
        private readonly RoleRepository roleRepository;
        private readonly IMapper mapper;
        public MasterDataService(
            IMapper mapper,
            FundRepository fundRepository,
            FundHouseRepository fundHouseRepository,
            BankAccountRepository bankAccountRepository,
            FundManagerRepository fundManagerRepository,
            RoleRepository roleRepository)
        {
            this.fundRepository = fundRepository;
            this.mapper = mapper;
            this.fundHouseRepository = fundHouseRepository;
            this.bankAccountRepository = bankAccountRepository;
            this.fundManagerRepository = fundManagerRepository;
            this.roleRepository = roleRepository;
        }

        public async Task<IEnumerable<FundHouseDto>> GetAllFundHousesAsync()
        {
            return mapper.Map<IEnumerable<FundHouseDto>>(await this.fundHouseRepository.GetAllAsync());
        }

        public async Task<IEnumerable<FundDto>> GetFundsAsync(int? fundHouseId)
        {
            return mapper.Map<IEnumerable<FundDto>>(await this.fundRepository.GetFundsAsync(fundHouseId));
        }

        public async Task<IEnumerable<BankAccountDto>> GetBankAccountsAsync(int? fundId)
        {
            return mapper.Map<IEnumerable<BankAccountDto>>(await this.bankAccountRepository.GetBankAccountsAsync(fundId));
        }

        public async Task<IEnumerable<FundManagerDto>> GetFundManagersAsync(int fundId)
        {
            return mapper.Map<IEnumerable<FundManagerDto>>(await this.fundManagerRepository.GetFundManagersAsync(fundId));
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            return mapper.Map<IEnumerable<RoleDto>>(await this.roleRepository.GetAllRolesAsync());
        }

        public async Task AddOrUpdateFundHouseAsync(FundHouseDto fundHouse, User user)
        {
            await fundHouseRepository.CreateOrUpdateAsync(mapper.Map<FundHouse>(fundHouse), user);
        }

        public async Task<bool> DoesFundHouseExistAsync(string fundHouse)
        {
            return await fundHouseRepository.DoesFundHouseExistsAsync(fundHouse);
        }

        public async Task<bool> DoesFundExistAsync(string fundName, int fundHouseId)
        {
            return await fundRepository.DoesFundExistsAsync(fundName, fundHouseId);
        }

        public async Task AddOrUpdateFundAsync(FundDto fund, User user)
        {
            await fundRepository.CreateOrUpdateAsync(mapper.Map<Fund>(fund), user);
        }
    }
}
