/*
 Ví dụ POCO Model cho Fluent API:
 - Không dùng Attribute ([Table], [Key], [Required]).
 - Cấu hình định nghĩa tập trung tại FluentApiDbContext.OnModelCreating().
*/

namespace examBlazor.Data.FluentApiModels;

public class CategoryFluent
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ICollection<ProductFluent> Products { get; set; } = new List<ProductFluent>();
}
