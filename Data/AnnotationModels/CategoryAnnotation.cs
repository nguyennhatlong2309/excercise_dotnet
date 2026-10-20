using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace examBlazor.Data.AnnotationModels;

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
