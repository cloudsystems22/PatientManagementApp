using PatientManagement.App.Domain.Common;
using PatientManagement.App.Domain.Dtos;

namespace PatientManagement.App.Domain.Interfaces;

public interface ICareApiClient
{
    Task<Result<IEnumerable<CareDto>>> SearchAsync(string? searchTerm = null,
        CancellationToken cancellationToken = default);

    Task<Result<CareDto>> GetByIdAsync(string id, CancellationToken cancellationToken = default);

    Task<Result<CareDto>> CreateAsync(CareDto care, CancellationToken cancellationToken = default);

    Task<Result<CareDto>> UpdateAsync(CareDto care, CancellationToken cancellationToken = default);

    Task<Result<bool>> DeleteAsync(string id, CancellationToken cancellationToken = default);
}