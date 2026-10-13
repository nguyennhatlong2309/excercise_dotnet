namespace examBlazor.Data.ConventionModels;

/// <summary>
/// CÁCH 1: CONVENTION (Quy ước ngầm định)
/// EF Core tự động nhận diện:
///   - "Id" -> Primary Key (Auto-Increment)
///   - ICollection<ProductConvention> -> Quan hệ 1 - N
///   - Không cần viết bất kỳ attribute hay Fluent API nào!
/// </summary>
public class CategoryConvention
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<ProductConvention> Products { get; set; } = new List<ProductConvention>();
}
