using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using KPFS.Business.Models;

namespace KPFS.Web.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ApiExceptionFilter> _logger;

        public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var configuration = context.HttpContext.RequestServices.GetService<IConfiguration>();
            context.Result = new ObjectResult(new ResponseDto<string>
            {
                IsSuccess = false,
                Message = context.Exception.ToString(),
            })
            { StatusCode = 500 };

            _logger.LogError(context.Exception.ToString());
        }
    }
}
