namespace examBlazor.Models.ViewModels;

public class DashboardViewModel
{
    public int TotalProducts { get; set; }
    public int TotalCategories { get; set; }
    public int TotalUsers { get; set; }
    public int TotalAdmins { get; set; }
    public decimal TotalInventoryValue { get; set; }
    public List<Product> LatestProducts { get; set; } = new();
    public List<Category> Categories { get; set; } = new();
}
