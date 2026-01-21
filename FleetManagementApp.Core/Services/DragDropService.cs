namespace FleetManagementApp.Core.Services;

public interface IDragDropService<T> {
    bool HasItem { get; }
    void StartDrag(T item);
    T? GetItem();
    void Clear();
}

public class DragDropService<T> : IDragDropService<T> where T : class {
    private T? _item;
    public bool HasItem => _item is not null;
    public void StartDrag(T item) => _item = item;
    public T? GetItem() => _item;
    public void Clear() => _item = null;
}