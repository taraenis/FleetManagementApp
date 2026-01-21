namespace FleetManagementApp.Core.Models.Domain.Fleets;

public sealed record Fleet(
    SingleShipDimensions SingleShipDimensions,
    string ShipDesignation,
    int ShipCount,
    int X,
    int Y
);