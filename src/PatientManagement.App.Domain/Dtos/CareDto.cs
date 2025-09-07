using System.Text.Json.Serialization;
using PatientManagement.App.Domain.Enums;

namespace PatientManagement.App.Domain.Dtos;

public class CareDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("sequenceNumber")]
    public string SequenceNumber { get; set; }

    [JsonPropertyName("patientId")]
    public string PatientId { get; set; }

    [JsonPropertyName("arrivalTime")]
    public DateTime ArrivalTime { get; set; }

    [JsonPropertyName("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusCare Status { get; set; }
}