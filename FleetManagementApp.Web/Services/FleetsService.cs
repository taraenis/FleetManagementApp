using FleetManagementApp.Core.Models.Api;
using FleetManagementApp.Core.Services.Api;

namespace FleetManagementApp.Core.Services;

public sealed class FleetsService(FleetsApiService fleetsApiService)
{
  public async Task<Result<FleetsApiResponse>> GetRandomFleetsAsync() {
    return await fleetsApiService.GetRandomFleetsAsync();
  }  
};