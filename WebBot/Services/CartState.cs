using WebBot.Models;

namespace WebBot.Services;

public class CartState
{
    private readonly List<CartItem> _items = new();
    public event Action? OnChange;

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();
    public int TotalItems => _items.Sum(i => i.Quantity);
    public decimal TotalPrice => _items.Sum(i => i.UnitPrice * i.Quantity);
    public bool IsEmpty => _items.Count == 0;
    public Guid? StoreId { get; private set; }
    public string? StoreName { get; private set; }

    public void Add(Product product, int quantity = 1)
    {
        var existing = _items.FirstOrDefault(i => i.ProductId == product.Id);
        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            _items.Add(new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                UnitPrice = product.Price,
                Quantity = quantity
            });
        }
        Notify();
    }

    public void UpdateQuantity(Guid productId, int quantity)
    {
        var item = _items.FirstOrDefault(i => i.ProductId == productId);
        if (item != null)
        {
            if (quantity <= 0)
                _items.Remove(item);
            else
                item.Quantity = quantity;
        }
        Notify();
    }

    public void Remove(Guid productId)
    {
        _items.RemoveAll(i => i.ProductId == productId);
        if (_items.Count == 0)
        {
            StoreId = null;
            StoreName = null;
        }
        Notify();
    }

    public void SetStore(Guid storeId, string storeName)
    {
        StoreId = storeId;
        StoreName = storeName;
    }

    public void Clear()
    {
        _items.Clear();
        StoreId = null;
        StoreName = null;
        Notify();
    }

    private void Notify() => OnChange?.Invoke();
}
