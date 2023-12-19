using KPFS.Business.Models;
using KPFS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace KPFS.Web.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public User CurrentUser
        {
            get
            {
                if (HttpContext == null || HttpContext.User == null)
                {
                    return null;
                }

                return _userManager.GetUserAsync(HttpContext.User).Result;
            }
        }
        public ApiBaseController(UserManager<User> userManager)
        {
            this._userManager = userManager;
        }

        public ActionResult<ResponseDto<TData>> BuildResponse<TData>(TData? data = default)
        {
            return StatusCode(StatusCodes.Status200OK, ResponseDto<TData>.GetSuccessResponse(data));
        }

        public ActionResult<ResponseDto<TData>> BuildFailureResponse<TData>(string message)
        {
            return StatusCode(StatusCodes.Status200OK, ResponseDto<TData>.GetFailureRespose(message));
        }
    }
}
