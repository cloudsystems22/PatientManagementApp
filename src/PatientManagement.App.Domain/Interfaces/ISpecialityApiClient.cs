using PatientManagement.App.Domain.Common;
using PatientManagement.App.Domain.Dtos;

namespace PatientManagement.App.Domain.Interfaces;

public interface ISpecialityApiClient
{
    Task<Result<IEnumerable<SpecialityDto>>> SearchAsync(string? searchTerm = null,
        CancellationToken cancellationToken = default);
}