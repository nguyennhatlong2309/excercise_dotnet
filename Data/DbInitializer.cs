using examBlazor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace examBlazor.Data;

public static class DbInitializer
{
    public static async Task SeedDataAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

        await context.Database.EnsureCreatedAsync();

        string[] roles = { "Admin", "User" };
        foreach (var r in roles)
        {
            if (!await roleManager.RoleExistsAsync(r))
            {
                await roleManager.CreateAsync(new ApplicationRole
                {
                    Name = r,
                    Description = r == "Admin" ? "Quản trị viên toàn quyền hệ thống" : "Khách hàng mua sắm"
                });
            }
        }

        if (!context.Categories.Any())
        {
            var categoryList = new List<Category>
            {
                new() { Name = "Điện thoại & Tablet", Description = "Smartphone, iPad, Tablet cao cấp", Icon = "📱" },
                new() { Name = "Laptop & Máy tính xách tay", Description = "Ultrabook, Laptop Gaming, Workstation", Icon = "💻" },
                new() { Name = "Màn hình máy tính", Description = "Màn hình 2K, 4K, OLED, tần số quét cao", Icon = "🖥️" },
                new() { Name = "Bàn phím cơ & Chuột", Description = "Bàn phím cơ Custom, chuột gaming siêu nhẹ", Icon = "⌨️" },
                new() { Name = "Tai nghe & Âm thanh", Description = "Tai nghe True Wireless, loa Bluetooth cao cấp", Icon = "🎧" },
                new() { Name = "Đồng hồ thông minh", Description = "Smartwatch đo sức khỏe, Apple Watch, Garmin", Icon = "⌚" },
                new() { Name = "Thiết bị mạng & Wi-Fi", Description = "Router Wi-Fi 6, Mesh Wi-Fi công suất lớn", Icon = "📡" },
                new() { Name = "Camera & Webcam", Description = "Webcam 4K streamer, máy ảnh vlog", Icon = "📷" },
                new() { Name = "Phụ kiện sạc & Cáp", Description = "Củ sạc GaN, pin sạc dự phòng, cáp Type-C", Icon = "🔌" },
                new() { Name = "Linh kiện PC & SSD", Description = "Ổ cứng SSD NVMe PCIe 4.0, RAM, tản nhiệt nước", Icon = "💾" }
            };

            await context.Categories.AddRangeAsync(categoryList);
            await context.SaveChangesAsync();
        }

        if (!context.Products.Any())
        {
            var cats = await context.Categories.OrderBy(c => c.Id).ToListAsync();

            var productList = new List<Product>
            {
                new()
                {
                    Name = "iPhone 16 Pro Max 256GB Desert Titanium",
                    Category = cats[0].Name,
                    CategoryId = cats[0].Id,
                    Brand = "Apple",
                    Price = 34990000,
                    OriginalPrice = 36990000,
                    Description = "Chip Apple A18 Pro 3nm, khung viền Titanium sa mạc sang trọng, camera zoom quang học 5x.",
                    ImageUrl = "📱",
                    Badge = "HOT",
                    Rating = 4.9,
                    ReviewCount = 148,
                    InStock = true
                },
                new()
                {
                    Name = "Samsung Galaxy S24 Ultra 512GB Titanium Gray",
                    Category = cats[0].Name,
                    CategoryId = cats[0].Id,
                    Brand = "Samsung",
                    Price = 31490000,
                    OriginalPrice = 34990000,
                    Description = "Quyền năng Galaxy AI, bút S-Pen tích hợp, camera 200MP siêu chi tiết, màn hình phẳng chống chói.",
                    ImageUrl = "📱",
                    Badge = "AI",
                    Rating = 4.8,
                    ReviewCount = 95,
                    InStock = true
                },
                new()
                {
                    Name = "MacBook Pro 14 M3 Pro (18GB RAM / 512GB SSD)",
                    Category = cats[1].Name,
                    CategoryId = cats[1].Id,
                    Brand = "Apple",
                    Price = 49990000,
                    OriginalPrice = 52990000,
                    Description = "Chip Apple M3 Pro 11-core CPU 14-core GPU, màn hình Liquid Retina XDR sáng 1600 nits đỉnh cao.",
                    ImageUrl = "💻",
                    Badge = "PRO",
                    Rating = 5.0,
                    ReviewCount = 62,
                    InStock = true
                },
                new()
                {
                    Name = "Dell XPS 13 Plus 9320 OLED Touch",
                    Category = cats[1].Name,
                    CategoryId = cats[1].Id,
                    Brand = "Dell",
                    Price = 38500000,
                    OriginalPrice = 42000000,
                    Description = "Thiết kế tương lai với hàng phím cảm ứng điện dung, touchpad kính vô hình, màn hình cảm ứng 3.5K OLED.",
                    ImageUrl = "💻",
                    Badge = "VIP",
                    Rating = 4.7,
                    ReviewCount = 41,
                    InStock = true
                },
                new()
                {
                    Name = "Màn hình LG UltraGear 27GR95QE OLED 240Hz",
                    Category = cats[2].Name,
                    CategoryId = cats[2].Id,
                    Brand = "LG",
                    Price = 19900000,
                    OriginalPrice = 22500000,
                    Description = "Tấm nền OLED chân thực, tần số quét 240Hz, thời gian phản hồi siêu tốc 0.03ms (GtG), hỗ trợ G-Sync.",
                    ImageUrl = "🖥️",
                    Badge = "GAMING",
                    Rating = 4.9,
                    ReviewCount = 83,
                    InStock = true
                },
                new()
                {
                    Name = "Bàn phím cơ Keychron Q1 Pro RGB Wireless",
                    Category = cats[3].Name,
                    CategoryId = cats[3].Id,
                    Brand = "Keychron",
                    Price = 4650000,
                    OriginalPrice = 4990000,
                    Description = "Vỏ nhôm CNC nguyên khối, kết nối Bluetooth 5.1 & dây Type-C, switch hot-swap, keycap PBT cao cấp.",
                    ImageUrl = "⌨️",
                    Badge = "BEST",
                    Rating = 4.9,
                    ReviewCount = 112,
                    InStock = true
                },
                new()
                {
                    Name = "Chuột công thái học Logitech MX Master 3S",
                    Category = cats[3].Name,
                    CategoryId = cats[3].Id,
                    Brand = "Logitech",
                    Price = 2190000,
                    OriginalPrice = 2490000,
                    Description = "Cảm biến Darkfield 8000 DPI lướt trên mọi mặt phẳng, click êm ái Quiet Clicks, cuộn MagSpeed siêu tốc.",
                    ImageUrl = "🖱️",
                    Badge = "WORK",
                    Rating = 4.8,
                    ReviewCount = 230,
                    InStock = true
                },
                new()
                {
                    Name = "Tai nghe chống ồn không dây Sony WH-1000XM5",
                    Category = cats[4].Name,
                    CategoryId = cats[4].Id,
                    Brand = "Sony",
                    Price = 7490000,
                    OriginalPrice = 8490000,
                    Description = "Chống ồn hàng đầu với bộ xử lý V1 và QN1, chất lượng đàm thoại chuẩn xác, pin 30 giờ nghe nhạc.",
                    ImageUrl = "🎧",
                    Badge = "HOT",
                    Rating = 4.9,
                    ReviewCount = 175,
                    InStock = true
                },
                new()
                {
                    Name = "Apple Watch Series 10 Nhôm GPS 46mm",
                    Category = cats[5].Name,
                    CategoryId = cats[5].Id,
                    Brand = "Apple",
                    Price = 10990000,
                    OriginalPrice = 11590000,
                    Description = "Màn hình OLED góc rộng sáng hơn 40%, mỏng hơn 10%, cảm biến độ sâu và nhiệt độ nước tích hợp.",
                    ImageUrl = "⌚",
                    Badge = "NEW",
                    Rating = 4.7,
                    ReviewCount = 88,
                    InStock = true
                },
                new()
                {
                    Name = "Router Wi-Fi 6 Gaming ASUS ROG Rapture GT-AX6000",
                    Category = cats[6].Name,
                    CategoryId = cats[6].Id,
                    Brand = "ASUS",
                    Price = 7200000,
                    OriginalPrice = 7990000,
                    Description = "Băng thông Wi-Fi 6 tốc độ 6000 Mbps, 2 cổng mạng 2.5G kép, tăng tốc game đa cấp độ.",
                    ImageUrl = "📡",
                    Badge = "FAST",
                    Rating = 4.8,
                    ReviewCount = 37,
                    InStock = true
                },
                new()
                {
                    Name = "Webcam Elgato Facecam Pro 4K60 Ultra HD",
                    Category = cats[7].Name,
                    CategoryId = cats[7].Id,
                    Brand = "Elgato",
                    Price = 7890000,
                    OriginalPrice = 8500000,
                    Description = "Webcam đầu tiên trên thế giới quay 4K ở 60fps chân thực, cảm biến Sony STARVIS lớn, ống kính f/2.0.",
                    ImageUrl = "📷",
                    Badge = "PRO",
                    Rating = 4.9,
                    ReviewCount = 29,
                    InStock = true
                },
                new()
                {
                    Name = "Củ sạc nhanh Anker Prime GaN 100W 3 cổng",
                    Category = cats[8].Name,
                    CategoryId = cats[8].Id,
                    Brand = "Anker",
                    Price = 1350000,
                    OriginalPrice = 1690000,
                    Description = "Công nghệ sạc GaNPrime độc quyền, 2 cổng USB-C và 1 cổng USB-A, sạc cùng lúc MacBook, iPhone và iPad.",
                    ImageUrl = "🔌",
                    Badge = "SALE",
                    Rating = 4.8,
                    ReviewCount = 310,
                    InStock = true
                }
            };

            await context.Products.AddRangeAsync(productList);
            await context.SaveChangesAsync();
        }

        if (!userManager.Users.Any())
        {
            var userSeedList = new List<(string username, string email, string fullName, string role)>
            {
                ("admin", "admin@shop.vn", "Nguyễn Nhật Long (Admin)", "Admin"),
                ("admin_manager", "manager@shop.vn", "Trần Quản Lý", "Admin"),
                ("admin_tech", "tech@shop.vn", "Lê Kỹ Thuật", "Admin"),
                ("user_an", "an.nguyen@gmail.com", "Nguyễn Văn An", "User"),
                ("user_bich", "bich.tran@gmail.com", "Trần Thị Bích", "User"),
                ("user_cuong", "cuong.le@gmail.com", "Lê Hoàng Cường", "User"),
                ("user_dung", "dung.pham@gmail.com", "Phạm Minh Dũng", "User"),
                ("user_hoa", "hoa.vo@gmail.com", "Võ Thị Quỳnh Hoa", "User"),
                ("user_khanh", "khanh.dang@gmail.com", "Đặng Quốc Khánh", "User"),
                ("user_lan", "lan.vu@gmail.com", "Vũ Ngọc Lan", "User")
            };

            foreach (var item in userSeedList)
            {
                var user = new ApplicationUser
                {
                    UserName = item.username,
                    Email = item.email,
                    FullName = item.fullName,
                    EmailConfirmed = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await userManager.CreateAsync(user, "Abc@123456");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, item.role);
                }
            }
        }
    }
}
