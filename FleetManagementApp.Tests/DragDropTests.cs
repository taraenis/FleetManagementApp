using FleetManagementApp.Core.Services;

namespace FleetManagementApp.Tests;

public class DragDropServiceTests
{
    [Fact]
    public void HasItem_IsFalse_Initially()
    {
        var service = new DragDropService<string>();
        Assert.False(service.HasItem);
        Assert.Null(service.GetItem());
    }

    [Fact]
    public void StartDrag_SetsItem()
    {
        var service = new DragDropService<string>();
        service.StartDrag("Ship-A");
        Assert.True(service.HasItem);
        Assert.Equal("Ship-A", service.GetItem());
    }

    [Fact]
    public void GetItem_ReturnsNull_WhenNoItem()
    {
        var service = new DragDropService<string>();
        Assert.False(service.HasItem);
        Assert.Null(service.GetItem());
    }

    [Fact]
    public void Clear_RemovesItem()
    {
        var service = new DragDropService<string>();
        service.StartDrag("Ship-A");
        service.Clear();
        Assert.False(service.HasItem);
        Assert.Null(service.GetItem());
    }

    [Fact]
    public void StartDrag_OverridesPreviousItem()
    {
        var service = new DragDropService<string>();
        service.StartDrag("Ship-A");
        service.StartDrag("Ship-B");
        Assert.True(service.HasItem);
        Assert.Equal("Ship-B", service.GetItem());
    }
}