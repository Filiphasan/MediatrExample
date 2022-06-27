using System.Text.Json.Serialization;

namespace MediatrExample.Shared.DataModels
{
    public class GenericResponse<TResponse>
    {
        [JsonPropertyName("statusCode")]
        public int HttpCode { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; } = String.Empty;
        [JsonPropertyName("data")]
        public TResponse? Response { get; set; }
        public IReadOnlyDictionary<string, string[]>? ValidationErrors { get; set; }
        public string[]? Errors { get; set; }

        public static GenericResponse<TResponse> Success(TResponse data)
        {
            return new GenericResponse<TResponse> { Status = true, Response = data, Message = "Success", HttpCode = 200 };
        }

        public static GenericResponse<TResponse> Success(int httpCode, TResponse data)
        {
            return new GenericResponse<TResponse> { Status = true, Response = data, Message = "Success", HttpCode = httpCode };
        }

        public static GenericResponse<TResponse> Success(int httpCode, string message, TResponse data)
        {
            return new GenericResponse<TResponse> { Status = true, Response = data, Message = message, HttpCode = httpCode };
        }

        public static GenericResponse<TResponse> Error(int httpCode, params string[] errors)
        {
            return new GenericResponse<TResponse> { Status = false, Message = "Error", HttpCode = httpCode, Errors = errors };
        }
    }
}
