using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using PatientManagement.App.Domain.Common;
using PatientManagement.App.Domain.Dtos;
using PatientManagement.App.Domain.Interfaces;

namespace PatientManagement.App.Client.Api;

public class TriageApiClient : ITriageApiClient
{
    private const string BaseUrl = "/api/v1/triage";
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public TriageApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<Result<IEnumerable<TriageDto>>> SearchAsync(string? searchTerm = null, CancellationToken cancellationToken = default)
    {
        try
        {
            var url = $"{BaseUrl}/search";
            if (!string.IsNullOrWhiteSpace(searchTerm))
                url += $"?searchTerm={searchTerm}";

            var response = await _httpClient.GetAsync(url, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var wrapper = JsonSerializer.Deserialize<Result<IEnumerable<TriageDto>>>(content, _jsonSerializerOptions);
                return wrapper!;
            }

            return Result<IEnumerable<TriageDto>>.Fail("API returned an error.");
        }
        catch (HttpRequestException ex)
        {
            return Result<IEnumerable<TriageDto>>.Fail($"HTTP error: {ex.Message}");
        }
    }

    public async Task<Result<TriageDto>> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}", cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var wrapper = JsonSerializer.Deserialize<Result<TriageDto>>(content, _jsonSerializerOptions);
                return wrapper!;
            }

            return Result<TriageDto>.Fail("API returned an error.");
        }
        catch (HttpRequestException ex)
        {
            return Result<TriageDto>.Fail($"HTTP error: {ex.Message}");
        }
    }

    public async Task<Result<TriageDto>> CreateAsync(TriageDto triage, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(BaseUrl, triage, _jsonSerializerOptions, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var wrapper = JsonSerializer.Deserialize<Result<TriageDto>>(content, _jsonSerializerOptions);
                return wrapper!;
            }

            return Result<TriageDto>.Fail("API returned an error.");
        }
        catch (HttpRequestException ex)
        {
            return Result<TriageDto>.Fail($"HTTP error: {ex.Message}");
        }
    }

    public async Task<Result<TriageDto>> UpdateAsync(TriageDto triage, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync(BaseUrl, triage, _jsonSerializerOptions, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync(cancellationToken);
                var wrapper = JsonSerializer.Deserialize<Result<TriageDto>>(content, _jsonSerializerOptions);
                return wrapper!;
            }

            return Result<TriageDto>.Fail("API returned an error.");
        }
        catch (HttpRequestException ex)
        {
            return Result<TriageDto>.Fail($"HTTP error: {ex.Message}");
        }
    }

    public async Task<Result<bool>> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}", cancellationToken);
        return response.IsSuccessStatusCode
            ? Result<bool>.Ok(true)
            : Result<bool>.Fail("API returned an error during deletion.");
    }
}