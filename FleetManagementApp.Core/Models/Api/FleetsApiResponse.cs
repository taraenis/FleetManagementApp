using FleetManagementApp.Core.Models.Domain.Fleets;

namespace FleetManagementApp.Core.Models.Api;

public sealed record FleetsApiResponse(
    SingleShipDimensions AnchorageSize,
    List<Fleet> Fleets
);