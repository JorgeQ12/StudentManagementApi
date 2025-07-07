using System.Data;

namespace Application.DTOs.Common
{
    public class ResultRequestDTO<T>
    {
        public bool IsSuccess { get; }
        public T? Value { get; }
        public string? Error { get; }

        private ResultRequestDTO(bool isSuccess, T? value, string? error)
        {
            IsSuccess = isSuccess;
            Value = value;
            Error = error;
        }

        public static ResultRequestDTO<T> Success(T value) => new(true, value, null);
        public static ResultRequestDTO<T> Failure(string error) => new(false, default, error);

    }
}
