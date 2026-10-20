using examBlazor.Data;
using examBlazor.Models.ViewModels;
using examBlazor.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace examBlazor.Controllers;

public class CartController : Controller
{
    private const string CartSessionKey = "SHOPPING_CART";
    private readonly AppDbContext _context;

    public CartController(AppDbContext context)
    {
        _context = context;
    }

    private List<CartItemViewModel> GetCart()
    {
        return HttpContext.Session.GetObject<List<CartItemViewModel>>(CartSessionKey) ?? new List<CartItemViewModel>();
    }

    private void SaveCart(List<CartItemViewModel> cart)
    {
        HttpContext.Session.SetObject(CartSessionKey, cart);
    }

    public IActionResult Index()
    {
        var cart = GetCart();
        return View(cart);
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int productId, int quantity = 1)
    {
        var product = await _context.Products.FindAsync(productId);
        if (product == null)
        {
            return NotFound();
        }

        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.ProductId == productId);

        if (item != null)
        {
            item.Quantity += quantity;
        }
        else
        {
            cart.Add(new CartItemViewModel
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Category = product.Category,
                Quantity = quantity
            });
        }

        SaveCart(cart);
        TempData["SuccessMessage"] = $"Đã thêm \"{product.Name}\" vào giỏ hàng!";
        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult UpdateQuantity(int productId, int delta)
    {
        var cart = GetCart();
        var item = cart.FirstOrDefault(c => c.ProductId == productId);

        if (item != null)
        {
            item.Quantity += delta;
            if (item.Quantity <= 0)
            {
                cart.Remove(item);
            }
            SaveCart(cart);
        }

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Remove(int productId)
    {
        var cart = GetCart();
        cart.RemoveAll(c => c.ProductId == productId);
        SaveCart(cart);

        return RedirectToAction("Index");
    }

    [HttpPost]
    public IActionResult Clear()
    {
        HttpContext.Session.Remove(CartSessionKey);
        return RedirectToAction("Index");
    }

    [Authorize]
    [HttpGet]
    public IActionResult Checkout()
    {
        var cart = GetCart();
        if (!cart.Any())
        {
            TempData["ErrorMessage"] = "Giỏ hàng của bạn đang trống!";
            return RedirectToAction("Index");
        }
        return View(cart);
    }

    [Authorize]
    [HttpPost]
    [ActionName("Checkout")]
    [ValidateAntiForgeryToken]
    public IActionResult ProcessCheckout()
    {
        var cart = GetCart();
        if (!cart.Any())
        {
            return RedirectToAction("Index");
        }

        HttpContext.Session.Remove(CartSessionKey);
        TempData["SuccessMessage"] = "Chúc mừng bạn đã đặt hàng thành công! Đơn hàng đang được xử lý.";
        return RedirectToAction("OrderSuccess");
    }

    public IActionResult OrderSuccess()
    {
        return View();
    }
}
