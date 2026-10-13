namespace examBlazor.Models;

public class CartItem
{
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; } = 1;
    public string SelectedColor { get; set; } = "Mặc định";

    public decimal SubTotal => Product.Price * Quantity;
}
