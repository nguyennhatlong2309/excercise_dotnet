# Exam Blazor — E-Commerce & EF Core Configuration Demo

Dự án bài tập / đồ án xây dựng bằng **Blazor Web App (.NET 10)** kết hợp **Entity Framework Core (MySQL)**, mô phỏng trang bán hàng trực tuyến và minh họa 3 phương pháp cấu hình Database Schema trong EF Core.

---

## 🚀 Công nghệ sử dụng

- **Framework**: .NET 10 (Blazor Web App - Interactive Server Components)
- **ORM**: Entity Framework Core 9.0
- **Database**: MySQL 8.0 (kết nối qua `Pomelo.EntityFrameworkCore.MySql`)
- **Container**: Docker Compose
- **Giao diện**: HTML5, CSS3, Bootstrap 5

---

## 📌 Tính năng chính

### 1. Cửa hàng trực tuyến (E-Commerce)
- **Trang chủ & Danh sách sản phẩm**: Hiển thị danh mục và thẻ sản phẩm trực quan.
- **Chi tiết sản phẩm**: Xem thông tin chi tiết và tùy chọn mua hàng.
- **Giỏ hàng (Cart)**: Quản lý thêm, sửa, xóa sản phẩm trong giỏ hàng (`CartService`).

### 2. Demo 3 cách cấu hình EF Core (Code-First)
Minh họa 3 cách tiếp cận định nghĩa Schema & Quan hệ (1-N giữa Category và Product):
1. **Convention**: Sử dụng quy ước đặt tên mặc định của Entity Framework (`ConventionDbContext`).
2. **Data Annotations**: Sử dụng các thuộc tính (Attributes) trực tiếp trên Model class (`AnnotationDbContext`).
3. **Fluent API**: Cấu hình linh hoạt và tường minh trong phương thức `OnModelCreating()` (`FluentApiDbContext`).

---

## 🛠️ Hướng dẫn cài đặt & khởi chạy

### 1. Yêu cầu môi trường
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/) (để chạy MySQL)

### 2. Khởi động Cơ sở dữ liệu MySQL
Sử dụng Docker Compose để khởi chạy nhanh MySQL Server:
```bash
docker-compose up -d
```
> MySQL sẽ chạy ở cổng `3306`, tài khoản root mặc định: `root / root`.

### 3. Cấu hình chuỗi kết nối
Kiểm tra cấu hình kết nối trong file `appsettings.json`:
```json
"ConnectionStrings": {
  "ConventionDB": "Server=localhost;Port=3306;Database=ShopDB_Convention;User=root;Password=root;",
  "AnnotationDB": "Server=localhost;Port=3306;Database=ShopDB_Annotation;User=root;Password=root;",
  "FluentApiDB": "Server=localhost;Port=3306;Database=ShopDB_FluentApi;User=root;Password=root;"
}
```

### 4. Chạy ứng dụng
Dự án được cấu hình tự động tạo database và bảng (`EnsureCreated()`) khi khởi động:
```bash
dotnet run
```
Mở trình duyệt theo địa chỉ hiển thị trên terminal (mặc định: `https://localhost:5001` hoặc `http://localhost:5000`).

---

## 📁 Cấu trúc thư mục

```
examBlazor/
├── Components/
│   ├── Layout/              # MainLayout, NavMenu, ReconnectModal
│   ├── Pages/               # Home, Products, ProductDetail, Cart, EfDemo,...
│   └── ProductCard.razor    # Component tái sử dụng hiển thị sản phẩm
├── Data/
│   ├── AnnotationDbContext.cs
│   ├── ConventionDbContext.cs
│   └── FluentApiDbContext.cs
├── Models/                  # Models cho Cart, Product
├── Services/                # CartService, ProductService
├── wwwroot/                 # Static assets (CSS, Bootstrap)
├── appsettings.json         # Cấu hình connection string
├── docker-compose.yml       # Docker compose cho MySQL
└── Program.cs               # Cấu hình pipeline & DI container
```
