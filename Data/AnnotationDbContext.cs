using examBlazor.Data.AnnotationModels;
using Microsoft.EntityFrameworkCore;

namespace examBlazor.Data;

/// <summary>
/// CÁCH 2: DATA ANNOTATION — Đọc cấu hình từ các Attribute trên Model
/// Không cần viết OnModelCreating()
/// </summary>
public class AnnotationDbContext : DbContext
{
    public AnnotationDbContext(DbContextOptions<AnnotationDbContext> options) : base(options) { }

    public DbSet<CategoryAnnotation> CategoryAnnotations { get; set; }
    public DbSet<ProductAnnotation> ProductAnnotations { get; set; }
}
