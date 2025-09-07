using PatientManagement.App.Domain.Common;
using PatientManagement.App.Domain.Dtos;

namespace PatientManagement.App.Domain.Interfaces;

public interface ITriageApiClient
{
    Task<Result<IEnumerable<TriageDto>>> SearchAsync(string? searchTerm = null,
        CancellationToken cancellationToken = default);

    Task<Result<TriageDto>> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<Result<TriageDto>> CreateAsync(TriageDto triage, CancellationToken cancellationToken = default);

    Task<Result<TriageDto>> UpdateAsync(TriageDto triage, CancellationToken cancellationToken = default);

    Task<Result<bool>> DeleteAsync(string id, CancellationToken cancellationToken = default);
}