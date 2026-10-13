namespace examBlazor.Data.ConventionModels;

/// <summary>
/// CÁCH 1: CONVENTION (Quy ước ngầm định)
/// </summary>
public class ProductConvention
{
    // Convention: "Id" -> PK, auto-increment
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    // Convention: Foreign Key = Tên Class Navigation + "Id"
    public int CategoryConventionId { get; set; }
    public CategoryConvention Category { get; set; } = null!;
}
