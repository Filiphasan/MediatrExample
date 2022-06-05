using System.Text.Json.Serialization;

namespace MediatrExample.Shared.DataModels
{
    public class GenericResponse<TResponse>
    {
        [JsonPropertyName("statusCode")]
        public int HttpCode { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        [JsonPropertyName("data")]
        public TResponse? Response { get; set; }
        public IReadOnlyDictionary<string, string[]> ValidationErrors { get; set; }

        public static GenericResponse<TResponse> Success(int httpCode, TResponse data)
        {
            return new GenericResponse<TResponse> { Status = true, Response = data, Message = "İşlem Başarılı", HttpCode = httpCode };
        }

        public static GenericResponse<TResponse> Success(int httpCode, string message, TResponse data)
        {
            return new GenericResponse<TResponse> { Status = true, Response = data, Message = message, HttpCode = httpCode };
        }

        public static GenericResponse<TResponse> Error(int httpCode, string message)
        {
            return new GenericResponse<TResponse> { Status = false, Message = message, HttpCode = httpCode };
        }
    }
}
