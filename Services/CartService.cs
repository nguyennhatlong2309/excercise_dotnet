using examBlazor.Models;

namespace examBlazor.Services;

public class CartService
{
    private readonly List<CartItem> _items = new();

    public event Action? OnChange;
    public string? LastAddedProductName { get; private set; }
    public bool ShowNotification { get; private set; }

    public IReadOnlyList<CartItem> GetCartItems() => _items.AsReadOnly();

    public void AddToCart(Product product, int quantity = 1, string color = "Mặc định")
    {
        var existing = _items.FirstOrDefault(i => i.Product.Id == product.Id && i.SelectedColor == color);
        if (existing != null)
        {
            existing.Quantity += quantity;
        }
        else
        {
            _items.Add(new CartItem
            {
                Product = product,
                Quantity = quantity,
                SelectedColor = color
            });
        }

        LastAddedProductName = product.Name;
        ShowNotification = true;
        NotifyStateChanged();
    }

    public void DismissNotification()
    {
        ShowNotification = false;
        NotifyStateChanged();
    }

    public void UpdateQuantity(int productId, int quantity)
    {
        var item = _items.FirstOrDefault(i => i.Product.Id == productId);
        if (item != null)
        {
            if (quantity <= 0)
            {
                _items.Remove(item);
            }
            else
            {
                item.Quantity = quantity;
            }
            NotifyStateChanged();
        }
    }

    public void RemoveFromCart(int productId)
    {
        _items.RemoveAll(i => i.Product.Id == productId);
        NotifyStateChanged();
    }

    public void ClearCart()
    {
        _items.Clear();
        NotifyStateChanged();
    }

    public decimal GetTotal() => _items.Sum(i => i.SubTotal);

    public int GetTotalCount() => _items.Sum(i => i.Quantity);

    private void NotifyStateChanged() => OnChange?.Invoke();
}
