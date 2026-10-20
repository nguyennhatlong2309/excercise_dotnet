using examBlazor.Data.AnnotationModels;
using Microsoft.EntityFrameworkCore;

namespace examBlazor.Data;

public class AnnotationDbContext : DbContext
{
    public AnnotationDbContext(DbContextOptions<AnnotationDbContext> options) : base(options) { }

    public DbSet<CategoryAnnotation> CategoryAnnotations { get; set; }
    public DbSet<ProductAnnotation> ProductAnnotations { get; set; }
}
