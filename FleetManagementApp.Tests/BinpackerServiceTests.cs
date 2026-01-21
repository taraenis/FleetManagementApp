using FleetManagementApp.Core.Models.Domain.Fleets;
using FleetManagementApp.Core.Services;

namespace FleetManagementApp.Tests;

public class BinPackerTests
{
    [Fact]
    public void TryPlace_FirstShip_IsPlacedAtOrigin()
    {
        var packer = new BinpackerService();
        packer.Initialize(10, 10);
        var ship = new SingleShipDimensions(2, 3);
        var placed = packer.TryPlace("A", ship);
        Assert.NotNull(placed);
        Assert.Equal(0, placed!.X);
        Assert.Equal(0, placed.Y);
        Assert.False(placed.IsRotated);
    }

    [Fact]
    public void TryPlace_DoesNotOverlapExistingShips()
    {
        var packer = new BinpackerService();
        packer.Initialize(10, 10);
        var ship = new SingleShipDimensions(5, 5);
        var first = packer.TryPlace("A", ship);
        var second = packer.TryPlace("B", ship);
        Assert.NotNull(first);
        Assert.NotNull(second);
        bool overlaps =
            second!.X < first!.X + first.Width &&
            second.X + second.Width > first.X &&
            second.Y < first.Y + first.Height &&
            second.Y + second.Height > first.Y;
        Assert.False(overlaps);
    }

    [Fact]
    public void TryPlace_ReturnsNull_WhenShipDoesNotFit()
    {
        var packer = new BinpackerService();
        packer.Initialize(4, 4);
        var largeShip = new SingleShipDimensions(5, 5);
        var placed = packer.TryPlace("TooBig", largeShip);
        Assert.Null(placed);
        Assert.Empty(packer.PlacedShips);
    }

    [Fact]
    public void TryPlace_Fails_WhenRotationDisabled()
    {
        var packer = new BinpackerService();
        packer.Initialize(3, 5);
        var ship = new SingleShipDimensions(5, 3);
        var placed = packer.TryPlace("NoRotate", ship, allowRotate: false);
        Assert.Null(placed);
    }

    [Fact]
    public void UpdateShip_ReplacesExistingShip()
    {
        var packer = new BinpackerService();
        packer.Initialize(10, 10);
        var ship = new SingleShipDimensions(2, 2);
        var original = packer.TryPlace("A", ship)!;
        var updated = original with { X = 5, Y = 5 };
        packer.UpdateShip(original, updated);
        Assert.Contains(updated, packer.PlacedShips);
        Assert.DoesNotContain(original, packer.PlacedShips);
    }

    [Fact]
    public void CanPlace_ReturnsFalse_OnCollision()
    {
        var packer = new BinpackerService();
        packer.Initialize(10, 10);
        var ship = new SingleShipDimensions(4, 4);
        var placed = packer.TryPlace("A", ship)!;
        var candidate = placed with { X = 2, Y = 2 };
        var canPlace = packer.CanPlace(candidate, candidate.X, candidate.Y);
        Assert.False(canPlace);
    }
}