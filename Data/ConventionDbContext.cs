using examBlazor.Data.ConventionModels;
using Microsoft.EntityFrameworkCore;

namespace examBlazor.Data;

public class ConventionDbContext : DbContext
{
    public ConventionDbContext(DbContextOptions<ConventionDbContext> options) : base(options) { }

    public DbSet<CategoryConvention> CategoryConventions { get; set; }
    public DbSet<ProductConvention> ProductConventions { get; set; }
}
