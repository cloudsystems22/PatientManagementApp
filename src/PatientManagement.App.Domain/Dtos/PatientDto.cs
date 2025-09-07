using PatientManagement.App.Domain.Enums;

namespace PatientManagement.App.Domain.Dtos;

public class PatientDto
{
    public string Id { get; set; }
    public string Rg { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public Gender Sex { get; set; }
    public string EmailAddress { get; set; } = string.Empty;
}