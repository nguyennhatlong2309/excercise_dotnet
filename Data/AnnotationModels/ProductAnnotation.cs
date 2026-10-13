using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examBlazor.Data.AnnotationModels;

/// <summary>
/// CÁCH 2: DATA ANNOTATION (Thuộc tính chú thích)
/// </summary>
[Table("tbl_Products")]
public class ProductAnnotation
{
    [Key]
    [Column("prod_id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    [Column("prod_name")]
    public string Name { get; set; } = string.Empty;

    [MaxLength(1000)]
    [Column("prod_description")]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column("prod_price", TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }

    [MaxLength(500)]
    [Column("prod_image_url")]
    public string ImageUrl { get; set; } = string.Empty;

    [ForeignKey("Category")]
    [Column("cat_id")]
    public int CategoryId { get; set; }
    public CategoryAnnotation Category { get; set; } = null!;
}
