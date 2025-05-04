namespace Data.Dtos
{
    public interface IServiceResult<T>
    {
        string? Error { get; set; }
        IEnumerable<T>? Result { get; set; }
        int StatusCode { get; set; }
        bool Succeeded { get; set; }

        static abstract ServiceResult<T> Fail(string error, int statusCode = 400);
        static abstract ServiceResult<T> Success(IEnumerable<T> result, int statusCode = 200);
    }

    public class ServiceResult<T> : IServiceResult<T>
    {
        public bool Succeeded { get; set; }
        public int StatusCode { get; set; }
        public string? Error { get; set; }
        public IEnumerable<T>? Result { get; set; }

        public static ServiceResult<T> Success(IEnumerable<T> result, int statusCode = 200)
        {
            return new ServiceResult<T>
            {
                Succeeded = true,
                StatusCode = statusCode,
                Result = result
            };
        }

        public static ServiceResult<T> Fail(string error, int statusCode = 400)
        {
            return new ServiceResult<T>
            {
                Succeeded = false,
                StatusCode = statusCode,
                Error = error
            };
        }
    }
}
