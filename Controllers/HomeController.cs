using examBlazor.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace examBlazor.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Categories = await _context.Categories.OrderBy(c => c.Name).ToListAsync();
        var featuredProducts = await _context.Products
            .Include(p => p.CategoryEntity)
            .Take(8)
            .ToListAsync();

        return View(featuredProducts);
    }

    public IActionResult Privacy()
    {
        return View();
    }
}
