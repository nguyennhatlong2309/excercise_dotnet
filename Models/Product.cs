using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examBlazor.Models;

/// <summary>
/// CÁCH 2: DATA ANNOTATION (Thuộc tính chú thích)
/// Dùng các attribute trong System.ComponentModel.DataAnnotations để:
///   - [Table("Products")]: Đổi tên bảng
///   - [Key], [DatabaseGenerated]: Chỉ định Primary Key tự tăng
///   - [Column("product_name")]: Đổi tên cột trong Database
///   - [Required], [MaxLength], [Range]: Ràng buộc dữ liệu & kiêm Form Validation
///   - [NotMapped]: Bỏ qua thuộc tính không lưu vào Database
/// </summary>
[Table("Products")]
public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [MaxLength(150, ErrorMessage = "Tối đa 150 ký tự")]
    [Column("product_name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Category { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Brand { get; set; } = string.Empty;

    [Required]
    [Range(1000, 1000000000, ErrorMessage = "Giá sản phẩm phải từ 1.000đ")]
    [Column("price", TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [Column("original_price", TypeName = "decimal(18,2)")]
    public decimal OriginalPrice { get; set; }

    [Range(0, 5)]
    public double Rating { get; set; } = 5.0;

    public int ReviewCount { get; set; } = 0;

    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(100)]
    public string Gradient { get; set; } = "linear-gradient(135deg, #6366f1 0%, #a855f7 100%)";

    [MaxLength(10)]
    public string IconGlyph { get; set; } = "📱";

    [MaxLength(20)]
    public string Badge { get; set; } = string.Empty;

    public bool InStock { get; set; } = true;

    // Các thuộc tính logic phục vụ giao diện, KHÔNG map vào DB
    [NotMapped]
    public List<string> Specs { get; set; } = new();

    [NotMapped]
    public List<string> Colors { get; set; } = new();

    [NotMapped]
    public string? ImageUrl { get; set; } = null;

    [NotMapped]
    public int DiscountPercentage => OriginalPrice > Price 
        ? (int)Math.Round((1 - Price / OriginalPrice) * 100) 
        : 0;
}
