using KPFS.Business.Models;

namespace KPFS.Web.ViewModels
{
    public sealed class ResponseViewModel<T> where T: DtoBase 
    {
        public static ResponseViewModel<T> GetSuccessResponse(T data)
        {
            return new ResponseViewModel<T>
            {
                Data = data,
                IsSuccess = true,
                Message = null
            };
        }

        public static ResponseViewModel<T> GetFailureRespose(string message, T? data = default)
        {
            return new ResponseViewModel<T>
            {
                Data = data,
                IsSuccess = false,
                Message = message
            };
        }

        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}
