using examBlazor.Data;
using examBlazor.Data.AnnotationModels;
using examBlazor.Models;
using Microsoft.EntityFrameworkCore;

namespace examBlazor.Services;

/// <summary>
/// ProductService: Truy vấn dữ liệu sản phẩm từ MySQL Database (ShopDB_Annotation) thông qua AnnotationDbContext
/// </summary>
public class ProductService
{
    private readonly IDbContextFactory<AnnotationDbContext> _dbFactory;

    public ProductService(IDbContextFactory<AnnotationDbContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    private static Product MapToProduct(ProductAnnotation p)
    {
        return new Product
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            Price = p.Price,
            OriginalPrice = p.Price * 1.15m,
            Category = p.Category != null ? p.Category.Name : "Công nghệ",
            Brand = "Nexus",
            Rating = 4.9,
            ReviewCount = 120,
            Gradient = "linear-gradient(135deg, #6366f1 0%, #a855f7 100%)",
            IconGlyph = p.Category?.Name == "Laptop" ? "💻" : p.Category?.Name == "Tai nghe" ? "🎧" : "📱",
            Badge = "HOT",
            InStock = true
        };
    }

    public List<Product> GetAllProducts()
    {
        using var db = _dbFactory.CreateDbContext();
        EnsureInitialData(db);
        return db.ProductAnnotations
                 .Include(p => p.Category)
                 .AsEnumerable()
                 .Select(MapToProduct)
                 .ToList();
    }

    public Product? GetProductById(int id)
    {
        using var db = _dbFactory.CreateDbContext();
        var item = db.ProductAnnotations
                     .Include(p => p.Category)
                     .FirstOrDefault(p => p.Id == id);
        return item != null ? MapToProduct(item) : null;
    }

    public List<string> GetCategories()
    {
        using var db = _dbFactory.CreateDbContext();
        var list = new List<string> { "Tất cả" };
        var cats = db.CategoryAnnotations.Select(c => c.Name).Distinct().ToList();
        list.AddRange(cats);
        return list;
    }

    public List<Product> SearchProducts(string? query, string? category, decimal? maxPrice)
    {
        var all = GetAllProducts();
        var result = all.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(category) && category != "Tất cả")
        {
            result = result.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

        if (maxPrice.HasValue && maxPrice.Value > 0)
        {
            result = result.Where(p => p.Price <= maxPrice.Value);
        }

        if (!string.IsNullOrWhiteSpace(query))
        {
            var text = query.Trim().ToLower();
            result = result.Where(p => p.Name.ToLower().Contains(text) ||
                                       p.Description.ToLower().Contains(text));
        }

        return result.ToList();
    }

    private void EnsureInitialData(AnnotationDbContext db)
    {
        if (!db.CategoryAnnotations.Any())
        {
            var catPhone = new CategoryAnnotation { Name = "Điện thoại" };
            var catLaptop = new CategoryAnnotation { Name = "Laptop" };
            var catAudio = new CategoryAnnotation { Name = "Tai nghe" };
            var catAccessory = new CategoryAnnotation { Name = "Phụ kiện" };

            db.CategoryAnnotations.AddRange(catPhone, catLaptop, catAudio, catAccessory);
            db.SaveChanges();

            db.ProductAnnotations.AddRange(
                new ProductAnnotation { Name = "Aura Phone 15 Pro Max", Description = "Flagship Titanium, chip A18 Bionic, màn hình 120Hz.", Price = 28990000, CategoryId = catPhone.Id },
                new ProductAnnotation { Name = "NexusBook Pro M3 Max", Description = "Đồ họa đỉnh cao, pin 22 giờ liên tục, màn hình Liquid Retina.", Price = 45990000, CategoryId = catLaptop.Id },
                new ProductAnnotation { Name = "SonicPods Ultra Wireless", Description = "Chống ồn ANC thế hệ mới, âm thanh vòm Spatial Audio.", Price = 4990000, CategoryId = catAudio.Id },
                new ProductAnnotation { Name = "Mechanical Keyboard Cyber 87", Description = "Bàn phím cơ không dây Hotswap, switch quang học, LED RGB.", Price = 2850000, CategoryId = catAccessory.Id }
            );
            db.SaveChanges();
        }
    }
}
