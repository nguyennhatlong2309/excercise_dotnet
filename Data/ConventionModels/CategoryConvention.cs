namespace examBlazor.Data.ConventionModels;

public class CategoryConvention
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<ProductConvention> Products { get; set; } = new List<ProductConvention>();
}
