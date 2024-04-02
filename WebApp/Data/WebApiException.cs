using System.Text.Json;

namespace WebApp.Data
{
    public class WebApiException(string errorJson) : Exception
    {
        public ErrorResponse? ErrorResponse { get; set; } = JsonSerializer.Deserialize<ErrorResponse>(errorJson);
    }
}
