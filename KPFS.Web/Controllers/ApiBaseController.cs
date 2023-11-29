using KPFS.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace KPFS.Web.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        public ActionResult<ResponseDto<TData>> BuildResponse<TData>(TData? data=default) 
        {
            return StatusCode(StatusCodes.Status200OK, ResponseDto<TData>.GetSuccessResponse(data));
        }

        public ActionResult<ResponseDto<TData>> BuildFailureResponse<TData>(string message) 
        {
            return StatusCode(StatusCodes.Status200OK, ResponseDto<TData>.GetFailureRespose(message));
        }
    }
}
