namespace FleetManagementApp.Core.Models.Domain.Fleets;

public sealed record PlacedShip(
    string ShipDesignation,
    SingleShipDimensions Dimensions,
    int X,
    int Y,
    bool IsRotated = false
) {
    public int Width => IsRotated ? Dimensions.Height : Dimensions.Width;
    public int Height => IsRotated ? Dimensions.Width : Dimensions.Height;

    public PlacedShip Rotate90() => this with { IsRotated = !IsRotated };
}