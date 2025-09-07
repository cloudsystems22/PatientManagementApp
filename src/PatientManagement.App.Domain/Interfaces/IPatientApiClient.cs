using PatientManagement.App.Domain.Common;
using PatientManagement.App.Domain.Dtos;

namespace PatientManagement.App.Domain.Interfaces;

public interface IPatientApiClient
{
    Task<Result<IEnumerable<PatientDto>>> SearchAsync(string? searchTerm = null,
        CancellationToken cancellationToken = default);

    Task<Result<PatientDto>> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<Result<PatientDto>> CreateAsync(PatientDto patient, CancellationToken cancellationToken = default);

    Task<Result<PatientDto>> UpdateAsync(PatientDto patient, CancellationToken cancellationToken = default);

    Task<Result<bool>> DeleteAsync(string id, CancellationToken cancellationToken = default);
}