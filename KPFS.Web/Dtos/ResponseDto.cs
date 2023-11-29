namespace KPFS.Business.Models
{
    public sealed class ResponseDto<TData>
    {
        public static ResponseDto<TData> GetSuccessResponse(TData? data = default)
        {
            return new ResponseDto<TData>
            {
                Data = data,
                IsSuccess = true,
                Message = null
            };
        }

        public static ResponseDto<TData> GetFailureRespose(string message)
        {
            return new ResponseDto<TData>
            {
                Data = default,
                IsSuccess = false,
                Message = message
            };
        }

        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public TData? Data { get; set; }
    }
}
