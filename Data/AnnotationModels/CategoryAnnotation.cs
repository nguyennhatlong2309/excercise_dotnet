using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examBlazor.Data.AnnotationModels;

/// <summary>
/// CÁCH 2: DATA ANNOTATION (Thuộc tính chú thích)
/// </summary>
[Table("tbl_Categories")]
public class CategoryAnnotation
{
    [Key]
    [Column("cat_id")]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("cat_name")]
    public string Name { get; set; } = string.Empty;

    public ICollection<ProductAnnotation> Products { get; set; } = new List<ProductAnnotation>();
}
