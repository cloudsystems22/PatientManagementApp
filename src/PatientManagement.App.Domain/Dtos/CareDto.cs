using PatientManagement.App.Domain.Enums;

namespace PatientManagement.App.Domain.Dtos;

public class CareDto
{
    public string Id { get; set; }
    public string SequenceNumber { get; set; }
    public string PatientId { get; set; }
    public DateTime ArrivalTime { get; set; }
    public StatusCare Status { get; set; }
}