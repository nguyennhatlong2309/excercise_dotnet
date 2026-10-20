using examBlazor.Data;
using examBlazor.Models;
using examBlazor.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace examBlazor.Controllers;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<IActionResult> Dashboard()
    {
        var users = await _userManager.Users.ToListAsync();
        var adminCount = 0;
        foreach (var u in users)
        {
            if (await _userManager.IsInRoleAsync(u, "Admin")) adminCount++;
        }

        var model = new DashboardViewModel
        {
            TotalProducts = await _context.Products.CountAsync(),
            TotalCategories = await _context.Categories.CountAsync(),
            TotalUsers = users.Count,
            TotalAdmins = adminCount,
            TotalInventoryValue = await _context.Products.SumAsync(p => p.Price),
            LatestProducts = await _context.Products.Include(p => p.CategoryEntity).OrderByDescending(p => p.Id).Take(5).ToListAsync(),
            Categories = await _context.Categories.Include(c => c.Products).ToListAsync()
        };

        return View(model);
    }

    public async Task<IActionResult> Products()
    {
        var list = await _context.Products
            .Include(p => p.CategoryEntity)
            .OrderByDescending(p => p.Id)
            .ToListAsync();
        return View(list);
    }

    [HttpGet]
    public async Task<IActionResult> CreateProduct()
    {
        ViewBag.CategoryList = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
        return View(new ProductViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateProduct(ProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.CategoryList = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", model.CategoryId);
            return View(model);
        }

        var cat = await _context.Categories.FindAsync(model.CategoryId);

        var product = new Product
        {
            Name = model.Name,
            CategoryId = model.CategoryId,
            Category = cat?.Name ?? "Công nghệ",
            Brand = model.Brand,
            Price = model.Price,
            OriginalPrice = model.OriginalPrice > 0 ? model.OriginalPrice : model.Price * 1.15m,
            Description = model.Description ?? string.Empty,
            ImageUrl = !string.IsNullOrWhiteSpace(model.ImageUrl) ? model.ImageUrl : "📱",
            Badge = model.Badge ?? "NEW",
            InStock = model.InStock,
            Rating = 5.0,
            ReviewCount = 1
        };

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        TempData["SuccessMessage"] = $"Đã thêm mới thành công sản phẩm: {product.Name}!";
        return RedirectToAction(nameof(Products));
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        var model = new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            CategoryId = product.CategoryId ?? 1,
            Brand = product.Brand,
            Price = product.Price,
            OriginalPrice = product.OriginalPrice,
            Description = product.Description,
            ImageUrl = product.ImageUrl,
            Badge = product.Badge,
            InStock = product.InStock
        };

        ViewBag.CategoryList = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", model.CategoryId);
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(int id, ProductViewModel model)
    {
        if (id != model.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            ViewBag.CategoryList = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name", model.CategoryId);
            return View(model);
        }

        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();

        var cat = await _context.Categories.FindAsync(model.CategoryId);

        product.Name = model.Name;
        product.CategoryId = model.CategoryId;
        product.Category = cat?.Name ?? "Công nghệ";
        product.Brand = model.Brand;
        product.Price = model.Price;
        product.OriginalPrice = model.OriginalPrice;
        product.Description = model.Description ?? string.Empty;
        product.ImageUrl = model.ImageUrl;
        product.Badge = model.Badge ?? "HOT";
        product.InStock = model.InStock;

        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = $"Đã cập nhật sản phẩm: {product.Name} thành công!";
        return RedirectToAction(nameof(Products));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = $"Đã xóa sản phẩm \"{product.Name}\" thành công!";
        }
        return RedirectToAction(nameof(Products));
    }

    public async Task<IActionResult> Users()
    {
        var users = await _userManager.Users.OrderBy(u => u.Id).ToListAsync();
        var userWithRoles = new List<(ApplicationUser User, string Role)>();

        foreach (var u in users)
        {
            var roles = await _userManager.GetRolesAsync(u);
            userWithRoles.Add((u, string.Join(", ", roles)));
        }

        return View(userWithRoles);
    }
}
