using System.Text.Json.Serialization;

namespace PatientManagement.App.Domain.Dtos;

public class SpecialityDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }
}