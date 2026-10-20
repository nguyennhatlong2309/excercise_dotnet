/*
 Ví dụ POCO Model cho Fluent API:
 - Khóa chính (Id) và khóa ngoại (CategoryId) ánh xạ qua FluentApiDbContext.
*/

namespace examBlazor.Data.FluentApiModels;

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
