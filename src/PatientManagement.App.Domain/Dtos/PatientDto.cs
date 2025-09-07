using System.Text.Json.Serialization;
using PatientManagement.App.Domain.Enums;

namespace PatientManagement.App.Domain.Dtos;

public class PatientDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("rg")]
    public string Rg { get; set; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("sex")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Gender Sex { get; set; }

    [JsonPropertyName("emailAddress")]
    public string EmailAddress { get; set; } = string.Empty;
}