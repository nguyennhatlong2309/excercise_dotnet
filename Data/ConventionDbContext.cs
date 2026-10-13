using examBlazor.Data.ConventionModels;
using Microsoft.EntityFrameworkCore;

namespace examBlazor.Data;

/// <summary>
/// CÁCH 1: CONVENTION — EF Core tự đọc quy ước đặt tên (Id -> PK, tên class -> table)
/// Không cần viết OnModelCreating()
/// </summary>
public class ConventionDbContext : DbContext
{
    public ConventionDbContext(DbContextOptions<ConventionDbContext> options) : base(options) { }

    public DbSet<CategoryConvention> CategoryConventions { get; set; }
    public DbSet<ProductConvention> ProductConventions { get; set; }
}
