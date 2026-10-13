using examBlazor.Data.FluentApiModels;
using Microsoft.EntityFrameworkCore;

namespace examBlazor.Data;

/// <summary>
/// CÁCH 3: FLUENT API — Toàn bộ cấu hình tập trung trong OnModelCreating()
/// </summary>
public class FluentApiDbContext : DbContext
{
    public FluentApiDbContext(DbContextOptions<FluentApiDbContext> options) : base(options) { }

    public DbSet<CategoryFluent> CategoryFluents { get; set; }
    public DbSet<ProductFluent> ProductFluents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // ===== CẤU HÌNH BẢNG Category =====
        modelBuilder.Entity<CategoryFluent>(entity =>
        {
            entity.ToTable("fluent_Categories");

            entity.HasKey(c => c.Id);                  // Primary Key

            entity.Property(c => c.Id)
                  .HasColumnName("cat_id")             // Tên cột
                  .ValueGeneratedOnAdd();               // Auto-increment

            entity.Property(c => c.Name)
                  .HasColumnName("cat_name")
                  .IsRequired()                        // NOT NULL
                  .HasMaxLength(100);                  // VARCHAR(100)
        });

        // ===== CẤU HÌNH BẢNG Product =====
        modelBuilder.Entity<ProductFluent>(entity =>
        {
            entity.ToTable("fluent_Products");

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                  .HasColumnName("prod_id")
                  .ValueGeneratedOnAdd();

            entity.Property(p => p.Name)
                  .HasColumnName("prod_name")
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(p => p.Description)
                  .HasColumnName("prod_description")
                  .HasMaxLength(1000);

            entity.Property(p => p.Price)
                  .HasColumnName("prod_price")
                  .HasColumnType("decimal(18,2)")      // Kiểu dữ liệu chính xác
                  .IsRequired();

            entity.Property(p => p.ImageUrl)
                  .HasColumnName("prod_image_url")
                  .HasMaxLength(500);

            entity.Property(p => p.CategoryId)
                  .HasColumnName("cat_id");            // Tên cột FK

            // Quan hệ 1-n: Category → Products
            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);   // Xóa category → xóa luôn products
        });
    }
}
