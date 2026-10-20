/*
 Ví dụ gọi REST API:
 - GET    /api/products           : Lấy danh sách sản phẩm (JSON)
 - GET    /api/products/1         : Lấy thông tin chi tiết sản phẩm ID = 1
 - POST   /api/products           : Tạo mới sản phẩm (Body: { "name": "...", "price": 100000, "categoryId": 1 })
 - PUT    /api/products/1         : Cập nhật sản phẩm ID = 1 (Body: { "name": "...", "price": 120000 })
 - DELETE /api/products/1         : Xóa sản phẩm ID = 1
*/

using examBlazor.Data;
using examBlazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace examBlazor.Controllers.Api;

[ApiController]
[Route("api/products")]
public class ProductsApiController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductsApiController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? categoryId, [FromQuery] string? search)
    {
        var query = _context.Products.AsQueryable();

        if (categoryId.HasValue && categoryId.Value > 0)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var s = search.Trim().ToLower();
            query = query.Where(p => p.Name.ToLower().Contains(s));
        }

        var list = await query.Select(p => new
        {
            p.Id,
            p.Name,
            p.Price,
            p.OriginalPrice,
            p.Category,
            p.CategoryId,
            p.Brand,
            p.ImageUrl,
            p.InStock
        }).ToListAsync();

        return Ok(list);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new { message = $"Không tìm thấy sản phẩm có ID = {id}" });
        }

        return Ok(product);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Product product)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] Product input)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new { message = $"Không tìm thấy sản phẩm có ID = {id}" });
        }

        product.Name = input.Name;
        product.Price = input.Price;
        product.CategoryId = input.CategoryId;
        product.Description = input.Description;
        product.InStock = input.InStock;

        await _context.SaveChangesAsync();
        return Ok(product);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null)
        {
            return NotFound(new { message = $"Không tìm thấy sản phẩm có ID = {id}" });
        }

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Xóa sản phẩm thành công", id });
    }
}
