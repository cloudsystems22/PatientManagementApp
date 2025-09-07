using System.Text.Json.Serialization;

namespace PatientManagement.App.Domain.Dtos;

public class TriageDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("careId")]
    public string CareId { get; set; }

    [JsonPropertyName("symptoms")]
    public string Symptoms { get; set; } = string.Empty;

    [JsonPropertyName("bloodPressure")]
    public string BloodPressure { get; set; } = string.Empty;

    [JsonPropertyName("weight")]
    public decimal Weight { get; set; }

    [JsonPropertyName("height")]
    public decimal Height { get; set; }

    [JsonPropertyName("specialtyId")]
    public string SpecialtyId { get; set; }

    [JsonPropertyName("imc")]
    public decimal IMC { get; set; }

    [JsonPropertyName("imcClassification")]
    public string IMCClassification { get; set; } = string.Empty;
}