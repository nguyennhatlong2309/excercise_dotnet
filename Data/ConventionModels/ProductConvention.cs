namespace examBlazor.Data.ConventionModels;

public class ProductConvention
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public int CategoryConventionId { get; set; }
    public CategoryConvention Category { get; set; } = null!;
}
