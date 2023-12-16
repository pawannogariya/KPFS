using KPFS.Business.Dtos;
using KPFS.Business.Models;
using KPFS.Business.Services.Interfaces;
using KPFS.Common;
using KPFS.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KPFS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MasterDataController : ApiBaseController
    {
        private readonly IMasterDataService masterDataService;
        private readonly MasterData _masterData;

        public MasterDataController(
            IMasterDataService masterDataService,
            UserManager<User> userManager,
            MasterData masterData) : base(userManager)
        {
            this.masterDataService = masterDataService;
            _masterData = masterData;
        }

        [HttpGet("fund-houses")]
        public async Task<ActionResult<ResponseDto<IEnumerable<FundHouseDto>>>> GetAllFundHouses()
        {
            return BuildResponse<IEnumerable<FundHouseDto>>(await masterDataService.GetAllFundHousesAsync());
        }

        [HttpGet("funds")]
        public async Task<ActionResult<ResponseDto<IEnumerable<FundDto>>>> GetAllFunds(int? fundHouseId)
        {
            return BuildResponse<IEnumerable<FundDto>>(await masterDataService.GetFundsAsync(fundHouseId));
        }

        [HttpGet("bank-accounts")]
        public async Task<ActionResult<ResponseDto<IEnumerable<BankAccountDto>>>> GetBankAccounts(int? fundId)
        {
            return BuildResponse<IEnumerable<BankAccountDto>>(await masterDataService.GetBankAccountsAsync(fundId));
        }

        [HttpGet("fund-managers")]
        public async Task<ActionResult<ResponseDto<IEnumerable<FundManagerDto>>>> GetFundManagers(int fundId)
        {
            return BuildResponse<IEnumerable<FundManagerDto>>(await masterDataService.GetFundManagersAsync(fundId));
        }

        [HttpGet("roles")]
        public async Task<ActionResult<ResponseDto<IEnumerable<RoleDto>>>> GetAllRoles()
        {
            return BuildResponse<IEnumerable<RoleDto>>(await masterDataService.GetAllRolesAsync());
        }

        [HttpGet("data")]
        public ActionResult<ResponseDto<MasterData>> GetMasterData()
        {
            return BuildResponse(_masterData);
        }
    }
}





