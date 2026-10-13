namespace examBlazor.Data.FluentApiModels;

/// <summary>
/// CÁCH 3: FLUENT API (Model POCO thuần)
/// Xem cấu hình chi tiết trong FluentApiDbContext.cs
/// </summary>
public class CategoryFluent
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<ProductFluent> Products { get; set; } = new List<ProductFluent>();
}
