using Newtonsoft.Json;

namespace BaseApplication.Dtos
{
    public class CommResultErrorDto
    {
        public bool IsSuccessful { get; set; } = false;
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Dữ liệu muốn truyền thêm khi có error
        /// VD: danh sách trùng khi check trùng
        /// </summary>
        public object ErrorData { get; set; }
        public T ConvertErrorData<T>()
        {
            if (ErrorData == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(ErrorData));
        }
    }
    public class CommonResultDto<T> : CommResultErrorDto
    {
        public T DataResult { get; set; }

        public CommonResultDto(T dataSuccess)
        {
            DataResult = dataSuccess;
            IsSuccessful = true;
            ErrorCode = string.Empty;
            ErrorMessage = string.Empty;
        }

        public CommonResultDto(string errorMessage)
        {
            IsSuccessful = false;
            ErrorMessage = errorMessage;
        }
        public CommonResultDto(string errorCode, string errorMessage)
        {
            IsSuccessful = false;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
        public CommonResultDto(string errorCode, string errorMessage,object errorData)
        {
            IsSuccessful = false;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            ErrorData = errorData;
        }

        public CommonResultDto() : base()
        {

        }
        public void SetDataSuccess(T data)
        {
            DataResult = data;
            IsSuccessful = true;
            ErrorCode = string.Empty;
            ErrorMessage = string.Empty;
        }
    }

    public static class CommResultHelper
    {
        public static CommResultErrorDto ErrorResult(string error)
        {
            return new CommResultErrorDto()
            {
                IsSuccessful = false,
                ErrorMessage = error
            };
        }
        public static CommResultErrorDto ErrorResult(string errorCode,string error)
        {
            return new CommResultErrorDto()
            {
                IsSuccessful = false,
                ErrorMessage = error,
                ErrorCode = errorCode
            };
        }
    }
}
