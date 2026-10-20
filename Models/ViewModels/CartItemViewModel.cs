namespace examBlazor.Models.ViewModels;

public class CartItemViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public string Category { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public decimal Total => Price * Quantity;
}
