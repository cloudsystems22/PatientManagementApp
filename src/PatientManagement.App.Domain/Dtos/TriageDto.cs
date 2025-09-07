namespace PatientManagement.App.Domain.Dtos;

public class TriageDto
{
    public string Id { get; set; }
    public string CareId { get; set; }
    public string Symptoms { get; set; } = string.Empty;
    public string BloodPressure { get; set; } = string.Empty;
    public decimal Weight { get; set; }
    public decimal Height { get; set; }
    public string SpecialtyId { get; set; }
    public decimal IMC { get; set; }
    public string IMCClassification { get; set; } = string.Empty;
}