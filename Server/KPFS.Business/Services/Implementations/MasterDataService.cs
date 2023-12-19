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
        private readonly FundRepository _fundRepository;
        private readonly FundHouseRepository _fundHouseRepository;
        private readonly BankAccountRepository _bankAccountRepository;
        private readonly FundManagerRepository _fundManagerRepository;
        private readonly RoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public MasterDataService(
            IMapper mapper,
            FundRepository fundRepository,
            FundHouseRepository fundHouseRepository,
            BankAccountRepository bankAccountRepository,
            FundManagerRepository fundManagerRepository,
            RoleRepository roleRepository)
        {
            this._fundRepository = fundRepository;
            this._mapper = mapper;
            this._fundHouseRepository = fundHouseRepository;
            this._bankAccountRepository = bankAccountRepository;
            this._fundManagerRepository = fundManagerRepository;
            this._roleRepository = roleRepository;
        }

        public async Task<IEnumerable<FundHouseDto>> GetAllFundHousesAsync()
        {
            return _mapper.Map<IEnumerable<FundHouseDto>>(await this._fundHouseRepository.GetAllAsync());
        }

        public async Task<IEnumerable<FundDto>> GetFundsAsync(int? fundHouseId)
        {
            return _mapper.Map<IEnumerable<FundDto>>(await this._fundRepository.GetFundsAsync(fundHouseId));
        }

        public async Task<IEnumerable<BankAccountDto>> GetBankAccountsAsync(int? fundId)
        {
            return _mapper.Map<IEnumerable<BankAccountDto>>(await this._bankAccountRepository.GetBankAccountsAsync(fundId));
        }

        public async Task<IEnumerable<FundManagerDto>> GetFundManagersAsync(int fundId)
        {
            return _mapper.Map<IEnumerable<FundManagerDto>>(await this._fundManagerRepository.GetFundManagersAsync(fundId));
        }

        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
        {
            return _mapper.Map<IEnumerable<RoleDto>>(await this._roleRepository.GetAllRolesAsync());
        }

        public async Task AddOrUpdateFundHouseAsync(FundHouseDto fundHouse, User user)
        {
            await _fundHouseRepository.CreateOrUpdateAsync(_mapper.Map<FundHouse>(fundHouse), user);
        }

        public async Task<bool> DoesFundHouseExistAsync(string fundHouse)
        {
            return await _fundHouseRepository.DoesFundHouseExistsAsync(fundHouse);
        }

        public async Task<bool> DoesFundExistAsync(string fundName, int fundHouseId)
        {
            return await _fundRepository.DoesFundExistsAsync(fundName, fundHouseId);
        }

        public async Task AddOrUpdateFundAsync(FundDto fund, User user)
        {
            await _fundRepository.CreateOrUpdateAsync(_mapper.Map<Fund>(fund), user);
        }
    }
}
