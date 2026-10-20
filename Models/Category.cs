using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examBlazor.Models;

[Table("Categories")]
public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên danh mục không được để trống")]
    [MaxLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự")]
    [Display(Name = "Tên danh mục")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(255)]
    [Display(Name = "Mô tả")]
    public string? Description { get; set; }

    [MaxLength(50)]
    [Display(Name = "Biểu tượng")]
    public string? Icon { get; set; } = "📁";

    public ICollection<Product> Products { get; set; } = new List<Product>();
}
