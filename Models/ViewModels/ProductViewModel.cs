using System.ComponentModel.DataAnnotations;

namespace examBlazor.Models.ViewModels;

public class ProductViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
    [StringLength(200, MinimumLength = 3, ErrorMessage = "Tên sản phẩm từ 3 đến 200 ký tự")]
    [Display(Name = "Tên sản phẩm")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Vui lòng chọn danh mục")]
    [Display(Name = "Danh mục")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Thương hiệu không được để trống")]
    [StringLength(100, ErrorMessage = "Tối đa 100 ký tự")]
    [Display(Name = "Thương hiệu")]
    public string Brand { get; set; } = "Nexus";

    [Required(ErrorMessage = "Giá bán là bắt buộc")]
    [Range(1000, 1000000000, ErrorMessage = "Giá bán phải từ 1.000đ đến 1.000.000.000đ")]
    [Display(Name = "Giá bán (VNĐ)")]
    public decimal Price { get; set; }

    [Display(Name = "Giá niêm yết (VNĐ)")]
    [Range(0, 1000000000, ErrorMessage = "Giá niêm yết không hợp lệ")]
    public decimal OriginalPrice { get; set; }

    [Display(Name = "Mô tả chi tiết")]
    [MaxLength(1000, ErrorMessage = "Mô tả tối đa 1000 ký tự")]
    public string? Description { get; set; }

    [Display(Name = "Hình ảnh (URL hoặc Icon)")]
    [MaxLength(500, ErrorMessage = "URL hình ảnh tối đa 500 ký tự")]
    public string? ImageUrl { get; set; } = "📱";

    [Display(Name = "Huy hiệu (Ví dụ: HOT, NEW, SALE)")]
    [MaxLength(20)]
    public string? Badge { get; set; } = "HOT";

    [Display(Name = "Tình trạng còn hàng")]
    public bool InStock { get; set; } = true;
}
