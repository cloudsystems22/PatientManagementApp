using System.Net.Http;
using System.Net.Http.Json;
using PatientManagement.App.Domain.Common;
using PatientManagement.App.Domain.Dtos;
using PatientManagement.App.Domain.Interfaces;

namespace PatientManagement.App.Client.Api;

public class SpecialityApiClient : ISpecialityApiClient
{
    private const string BaseUrl = "/api/v1/speciality";
    private readonly HttpClient _httpClient;

    public SpecialityApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<Result<IEnumerable<SpecialityDto>>> SearchAsync(string? searchTerm = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var url = $"{BaseUrl}/search";
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                url += $"?searchTerm={searchTerm}";
            }

            var result = await _httpClient.GetFromJsonAsync<Result<IEnumerable<SpecialityDto>>>(url, cancellationToken);

            return result ?? Result<IEnumerable<SpecialityDto>>.Fail("Failed to deserialize API response.");
        }
        catch (HttpRequestException ex)
        {
            // Log the exception details here if you have a logging mechanism.
            return Result<IEnumerable<SpecialityDto>>.Fail($"An error occurred while communicating with the API: {ex.Message}");
        }
    }
}