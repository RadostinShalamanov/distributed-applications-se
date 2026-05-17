using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace E_Commerce.API.Helpers
{
    public class ApiResponse<T>
    {
        public bool Success { get; private set; }
        public T? Data { get; private set; }
        public IEnumerable<string> Errors { get; private set; }

        private ApiResponse() { }

        public static ApiResponse<T> Ok(T data) => new()
        {
            Success = true,
            Data = data,
            Errors = Enumerable.Empty<string>()
        };

        public static ApiResponse<T> Fail(params string[] errors) => new()
        {
            Success = false,
            Data = default,
            Errors = errors
        };

        // Overload that pulls errors straight from ModelState
        public static ApiResponse<T> Fail(ModelStateDictionary modelState) => new()
        {
            Success = false,
            Data = default,
            Errors = modelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
        };
    }
}
