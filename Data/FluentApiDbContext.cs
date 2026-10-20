/*
 Ví dụ cú pháp Fluent API (EF Core):
 - Đổi tên bảng: modelBuilder.Entity<T>().ToTable("tbl_name");
 - Khóa chính:    modelBuilder.Entity<T>().HasKey(x => x.Id);
 - Ràng buộc cột: modelBuilder.Entity<T>().Property(x => x.Name).HasColumnName("name").HasMaxLength(100).IsRequired();
 - Quan hệ 1 - N: modelBuilder.Entity<Product>().HasOne(p => p.Category).WithMany(c => c.Products).HasForeignKey(p => p.CategoryId);
*/

using examBlazor.Data.FluentApiModels;
using Microsoft.EntityFrameworkCore;

namespace examBlazor.Data;

public class FluentApiDbContext : DbContext
{
    public FluentApiDbContext(DbContextOptions<FluentApiDbContext> options) : base(options) { }

    public DbSet<CategoryFluent> CategoryFluents { get; set; }
    public DbSet<ProductFluent> ProductFluents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CategoryFluent>(entity =>
        {
            entity.ToTable("fluent_Categories");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Id)
                  .HasColumnName("cat_id")
                  .ValueGeneratedOnAdd();

            entity.Property(c => c.Name)
                  .HasColumnName("cat_name")
                  .IsRequired()
                  .HasMaxLength(100);
        });

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
                  .HasColumnType("decimal(18,2)")
                  .IsRequired();

            entity.Property(p => p.ImageUrl)
                  .HasColumnName("prod_image_url")
                  .HasMaxLength(500);

            entity.Property(p => p.CategoryId)
                  .HasColumnName("cat_id");

            entity.HasOne(p => p.Category)
                  .WithMany(c => c.Products)
                  .HasForeignKey(p => p.CategoryId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
