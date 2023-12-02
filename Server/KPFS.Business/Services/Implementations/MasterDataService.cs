using AutoMapper;
using KPFS.Business.Dtos;
using KPFS.Business.Services.Interfaces;
using KPFS.Data.Repositories;

namespace KPFS.Business.Services.Implementations
{
    public class MasterDataService : IMasterDataService
    {
        private readonly FundRepository fundRepository;
        private readonly FundHouseRepository fundHouseRepository;
        private readonly BankAccountRepository bankAccountRepository;
        private readonly FundManagerRepository fundManagerRepository;
        private readonly IMapper mapper;
        public MasterDataService(
            IMapper mapper,
            FundRepository fundRepository,
            FundHouseRepository fundHouseRepository,
            BankAccountRepository bankAccountRepository, 
            FundManagerRepository fundManagerRepository)
        {
            this.fundRepository = fundRepository;
            this.mapper = mapper;
            this.fundHouseRepository = fundHouseRepository;
            this.bankAccountRepository = bankAccountRepository;
            this.fundManagerRepository = fundManagerRepository;
        }

        public async Task<IEnumerable<FundHouseDto>> GetAllFundHousesAsync()
        {
            return mapper.Map<IEnumerable<FundHouseDto>>(await this.fundHouseRepository.GetAllAsync());
        }

        public async Task<IEnumerable<FundDto>> GetAllFundsAsync()
        {
            return mapper.Map<IEnumerable<FundDto>>(await this.fundRepository.GetAllAsync());
        }

        public async Task<IEnumerable<BankAccountDto>> GetAllBankAccountsAsync()
        {
            return mapper.Map<IEnumerable<BankAccountDto>>(await this.bankAccountRepository.GetAllAsync());
        }

        public async Task<IEnumerable<BankAccountDto>> GetFundBankAccountsAsync(int fundId)
        {
            return mapper.Map<IEnumerable<BankAccountDto>>(await this.bankAccountRepository.GetFundBankAccountsAsync(fundId));
        }

        public async Task<IEnumerable<FundManagerDto>> GetFundManagersAsync(int fundId)
        {
            return mapper.Map<IEnumerable<FundManagerDto>>(await this.fundManagerRepository.GetFundManagersAsync(fundId));
        }
    }
}
