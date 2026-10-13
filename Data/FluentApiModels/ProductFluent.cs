namespace examBlazor.Data.FluentApiModels;

/// <summary>
/// CÁCH 3: FLUENT API (Model POCO thuần)
/// Xem cấu hình chi tiết trong FluentApiDbContext.cs
/// </summary>
public class ProductFluent
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public int CategoryId { get; set; }
    public CategoryFluent Category { get; set; } = null!;
}
