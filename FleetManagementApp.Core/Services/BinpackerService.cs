using FleetManagementApp.Core.Models.Domain.Fleets;

namespace FleetManagementApp.Core.Services;

public sealed class BinpackerService
{
    public int Width { get; private set; }
    public int Height { get; private set; }
    public List<PlacedShip> PlacedShips { get; } = [];

    public void Initialize(int width, int height) {
        Width = width;
        Height = height;
        Clear();
    }

    public PlacedShip? TryPlace(string designation, SingleShipDimensions dimensions, bool allowRotate = true)
    {
        var orientations = allowRotate
            ? [false, true]
            : new[] { false };

        foreach (var rotated in orientations) {
            var tempShip = new PlacedShip(designation, dimensions, 0, 0, rotated);
            
            for (var x = 0; x <= Width - tempShip.Width; x++) {
                for (var y = 0; y <= Height - tempShip.Height; y++) {
                    var candidate = tempShip with { X = x, Y = y };

                    if (IsOverlapping(candidate))
                    {
                        continue;
                    }

                    PlacedShips.Add(candidate);
                    return candidate;
                }
            }
        }

        return null;
    }

    public bool CanPlaceShip(string designation, SingleShipDimensions dimensions, bool allowRotate = true)
    {
        var orientations = allowRotate ? [false, true] : new[] { false };
        foreach (var rotated in orientations)
        {
            var tempShip = new PlacedShip(designation, dimensions, 0, 0, rotated);
            for (var x = 0; x <= Width - tempShip.Width; x++)
            {
                for (var y = 0; y <= Height - tempShip.Height; y++)
                {
                    var candidate = tempShip with { X = x, Y = y };
                    if (!IsOverlapping(candidate))
                    {
                        return true;   
                    }
                }
            }
        }

        return false;
    }
    private bool IsOverlapping(PlacedShip ship)
    {
        foreach (var placed in PlacedShips) {
            var horizontal = ship.X + ship.Width > placed.X && ship.X < placed.X + placed.Width;
            var vertical = ship.Y + ship.Height > placed.Y && ship.Y < placed.Y + placed.Height;

            if (horizontal && vertical) {
                return true;
            }
        }

        return false;
    }

    public void UpdateShip(PlacedShip oldShip, PlacedShip newShip)
    {
        var index = PlacedShips.FindIndex(s => s == oldShip);
        if (index != -1) {
            PlacedShips[index] = newShip;
        }
    }

    public bool CanPlace(PlacedShip ship, int x, int y, PlacedShip? ignoreShip = null)
    {
        if (x < 0 || y < 0 || x + ship.Width > Width || y + ship.Height > Height) {
            return false;
        }

        foreach (var placed in PlacedShips) {
            if (ignoreShip != null && placed == ignoreShip) {
                continue;
            }

            if (ship.X < placed.X + placed.Width &&
                ship.X + ship.Width > placed.X &&
                ship.Y < placed.Y + placed.Height &&
                ship.Y + ship.Height > placed.Y) {
                return false;
            }
        }

        return true;
    }

    public bool IsCompleted(IEnumerable<Fleet> remainingFleets)
    {
        if (!remainingFleets.Any())
        {
            return true;
        }

        return !remainingFleets.Any(f => f.ShipCount > 0 && CanPlaceShip(f.ShipDesignation, f.SingleShipDimensions, allowRotate: true));
    }


    public void Clear() => PlacedShips.Clear();
}