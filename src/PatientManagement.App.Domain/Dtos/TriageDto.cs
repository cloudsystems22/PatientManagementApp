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

    [JsonPropertyName("specialityId")]
    public string SpecialityId { get; set; }

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public decimal IMC => (Height > 0 && Weight > 0) ? Weight / (Height * Height) : 0;

    [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
    public string IMCClassification
    {
        get
        {
            var imc = this.IMC;
            if (imc == 0) return "Não calculado";
            if (imc < 18.5m) return "Abaixo do peso";
            if (imc < 25.0m) return "Peso normal";
            if (imc < 30.0m) return "Sobrepeso";
            if (imc < 35.0m) return "Obesidade Grau I";
            if (imc < 40.0m) return "Obesidade Grau II";
            if (imc >= 40.0m) return "Obesidade Grau III";
            return "Não calculado";
        }
    }
}