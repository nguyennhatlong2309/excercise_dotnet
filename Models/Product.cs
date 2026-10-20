using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examBlazor.Models;

[Table("Products")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [MaxLength(200, ErrorMessage = "Tối đa 200 ký tự")]
    [Display(Name = "Tên sản phẩm")]
    [Column("product_name")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(100)]
    [Display(Name = "Danh mục")]
    public string Category { get; set; } = "Công nghệ";

    [ForeignKey("CategoryEntity")]
    public int? CategoryId { get; set; }
    public Category? CategoryEntity { get; set; }

    [MaxLength(100)]
    [Display(Name = "Thương hiệu")]
    public string Brand { get; set; } = "Nexus";

    [Required(ErrorMessage = "Giá sản phẩm là bắt buộc")]
    [Range(1000, 1000000000, ErrorMessage = "Giá sản phẩm phải từ 1.000đ đến 1.000.000.000đ")]
    [Display(Name = "Giá bán")]
    [Column("price", TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Display(Name = "Giá niêm yết")]
    [Column("original_price", TypeName = "decimal(18,2)")]
    public decimal OriginalPrice { get; set; }

    [Range(0, 5)]
    [Display(Name = "Đánh giá")]
    public double Rating { get; set; } = 5.0;

    public int ReviewCount { get; set; } = 10;

    [MaxLength(1000)]
    [Display(Name = "Mô tả")]
    public string Description { get; set; } = string.Empty;

    [MaxLength(500)]
    [Display(Name = "Hình ảnh (URL hoặc Icon)")]
    public string? ImageUrl { get; set; } = "📱";

    [MaxLength(100)]
    public string Gradient { get; set; } = "linear-gradient(135deg, #6366f1 0%, #a855f7 100%)";

    [MaxLength(20)]
    public string IconGlyph { get; set; } = "📱";

    [MaxLength(50)]
    public string Badge { get; set; } = "HOT";

    [Display(Name = "Còn hàng")]
    public bool InStock { get; set; } = true;

    [NotMapped]
    public List<string> Specs { get; set; } = new();

    [NotMapped]
    public List<string> Colors { get; set; } = new();

    [NotMapped]
    public int DiscountPercentage => OriginalPrice > Price 
        ? (int)Math.Round((1 - Price / OriginalPrice) * 100) 
        : 0;
}
