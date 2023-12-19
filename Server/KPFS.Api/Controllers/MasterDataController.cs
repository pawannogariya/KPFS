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
        private readonly IMasterDataService _masterDataService;
        private readonly MasterData _masterData;

        public MasterDataController(
            IMasterDataService masterDataService,
            UserManager<User> userManager,
            MasterData masterData) : base(userManager)
        {
            this._masterDataService = masterDataService;
            _masterData = masterData;
        }

        [HttpGet("fund-house/list")]
        public async Task<ActionResult<ResponseDto<IEnumerable<FundHouseDto>>>> GetAllFundHouses()
        {
            return BuildResponse<IEnumerable<FundHouseDto>>(await _masterDataService.GetAllFundHousesAsync());
        }

        [HttpGet("fund/list")]
        public async Task<ActionResult<ResponseDto<IEnumerable<FundDto>>>> GetAllFunds(int? fundHouseId)
        {
            return BuildResponse<IEnumerable<FundDto>>(await _masterDataService.GetFundsAsync(fundHouseId));
        }

        [HttpGet("bank-account/list")]
        public async Task<ActionResult<ResponseDto<IEnumerable<BankAccountDto>>>> GetBankAccounts(int? fundId)
        {
            return BuildResponse<IEnumerable<BankAccountDto>>(await _masterDataService.GetBankAccountsAsync(fundId));
        }

        [HttpGet("fund-manager/list")]
        public async Task<ActionResult<ResponseDto<IEnumerable<FundManagerDto>>>> GetFundManagers(int fundId)
        {
            return BuildResponse<IEnumerable<FundManagerDto>>(await _masterDataService.GetFundManagersAsync(fundId));
        }

        [HttpGet("role/list")]
        public async Task<ActionResult<ResponseDto<IEnumerable<RoleDto>>>> GetAllRoles()
        {
            return BuildResponse<IEnumerable<RoleDto>>(await _masterDataService.GetAllRolesAsync());
        }

        [HttpGet("data")]
        public ActionResult<ResponseDto<MasterData>> GetMasterData()
        {
            return BuildResponse(_masterData);
        }
    }
}





