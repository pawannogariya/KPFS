using KPFS.Business.Dtos;
using KPFS.Business.Models;
using KPFS.Business.Services.Interfaces;
using KPFS.Data.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KPFS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = Roles.Admin)]
    public class MasterDataController : ApiBaseController
    {
        private readonly IMasterDataService masterDataService;

        public MasterDataController(IMasterDataService masterDataService)
        {
            this.masterDataService = masterDataService;
        }

        [HttpGet("fund-houses")]
        public async Task<ActionResult<ResponseDto<IEnumerable<FundHouseDto>>>> GetAllFundHouses()
        {
            return BuildResponse<IEnumerable<FundHouseDto>>(await masterDataService.GetAllFundHousesAsync());
        }

        [HttpGet("funds")]
        public async Task<ActionResult<ResponseDto<IEnumerable<FundDto>>>> GetAllFunds()
        {
            return BuildResponse<IEnumerable<FundDto>>(await masterDataService.GetAllFundsAsync());
        }

        [HttpGet("bank-accounts")]
        public async Task<ActionResult<ResponseDto<IEnumerable<BankAccountDto>>>> GetAllBankAccounts()
        {
            return BuildResponse<IEnumerable<BankAccountDto>>(await masterDataService.GetAllBankAccountsAsync());
        }

        [HttpGet("fund-bank-accounts")]
        public async Task<ActionResult<ResponseDto<IEnumerable<BankAccountDto>>>> GetFundBankAccounts(int fundId)
        {
            return BuildResponse<IEnumerable<BankAccountDto>>(await masterDataService.GetFundBankAccountsAsync(fundId));
        }


        [HttpGet("fund-managers")]
        public async Task<ActionResult<ResponseDto<IEnumerable<FundManagerDto>>>> GetFundManagers(int fundId)
        {
            return BuildResponse<IEnumerable<FundManagerDto>>(await masterDataService.GetFundManagersAsync(fundId));
        }
    }
}
