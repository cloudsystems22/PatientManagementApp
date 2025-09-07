using PatientManagement.App.Domain.Common;
using PatientManagement.App.Domain.Dtos;

namespace PatientManagement.App.Domain.Interfaces;

public interface ISpecialityApiClient
{
    Task<Result<IEnumerable<SpecialityDto>>> SearchAsync(string? searchTerm = null,
        CancellationToken cancellationToken = default);

    Task<Result<SpecialityDto>> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<Result<SpecialityDto>> CreateAsync(SpecialityDto speciality, CancellationToken cancellationToken = default);

    Task<Result<SpecialityDto>> UpdateAsync(SpecialityDto speciality,
        CancellationToken cancellationToken = default);

    Task<Result<bool>> DeleteAsync(string id, CancellationToken cancellationToken = default);
}