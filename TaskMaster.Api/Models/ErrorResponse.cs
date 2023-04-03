using System.Text.Json.Serialization;

namespace TaskMaster.Api.Models
{
    /// <summary>
    /// Represents a response object used to return an error message and status code in a standardized format.
    /// </summary>
    internal record ErrorResponse(string ErrorMessage, int StatusCode)
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? StackTrace { get; set; }
    }
}
