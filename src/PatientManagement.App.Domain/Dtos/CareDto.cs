﻿using System.Text.Json.Serialization;
using PatientManagement.App.Domain.Enums;

namespace PatientManagement.App.Domain.Dtos;

public class CareDto
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("sequenceNumber")]
    public string SequenceNumber { get; set; } = string.Empty;

    [JsonPropertyName("patientId")]
    public string PatientId { get; set; } = string.Empty;

    [JsonPropertyName("entryDate")]
    public DateTime? EntryDate { get; set; } = DateTime.Now;

    [JsonPropertyName("status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusCare Status { get; set; }
}