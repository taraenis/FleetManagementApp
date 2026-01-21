using System.Net.Http.Json;
using FleetManagementApp.Core.Models.Api;

namespace FleetManagementApp.Core.Services.Api;

public sealed class FleetsApiService(HttpClient http) {
    public async Task<Result<FleetsApiResponse>> GetRandomFleetsAsync() {
        try {
            var response = await http.GetFromJsonAsync<FleetsApiResponse>("fleets/random");
            return response is null ? Result<FleetsApiResponse>.Failure("Empty response") : Result<FleetsApiResponse>.Success(response);
        }
        catch (HttpRequestException ex) {
            return Result<FleetsApiResponse>.Failure($"HTTP error: {ex.Message}");
        }
    }
}