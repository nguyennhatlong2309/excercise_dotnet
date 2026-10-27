# Nexus Store — ASP.NET Core MVC Architecture Demo

Dự án bài tập / đồ án được xây dựng theo chuẩn mô hình **ASP.NET Core MVC (.NET 10)** kết hợp **ASP.NET Core Identity & Entity Framework Core**, đáp ứng đầy đủ các tiêu chuẩn đề bài:
1. **Mỗi bảng ít nhất 10 dòng dữ liệu** (Seed Data tự động).
2. **Phân quyền linh động**:
   - Kế thừa từ `IdentityUser<int>` (`ApplicationUser`) và `IdentityRole<int>` (`ApplicationRole`).
   - Tạo phiên làm việc với **Cookie Authentication** & **Session State** cho Giỏ hàng.
   - Lưu trữ người dùng, mật khẩu băm (PBKDF2) trong Database.
   - Đăng nhập phân quyền rõ rệt: Tài khoản quyền **Admin** tự động điều hướng về `/Admin/Dashboard`, quyền **User** điều hướng về `/Product`.
3. **Kiểm tra Validation (Check Validation)**:
   - Data Annotations trên Model & ViewModel.
   - Kiểm tra chặt chẽ `ModelState.IsValid` tại Controller. 
   - Client-side validation với `jquery.validate.unobtrusive`.

---

## 🚀 Tài khoản mẫu để kiểm thử phân quyền (Đã Seed sẵn)

Mật khẩu chung cho tất cả tài khoản mẫu: `Abc@123456`

| Quyền hạn | Tên đăng nhập (Username) | Họ và tên | Chuyển hướng khi đăng nhập |
|---|---|---|---|
| **Admin** | `admin` | Nguyễn Nhật Long (Admin) | ➡️ `/Admin/Dashboard` (Quản trị hệ thống) |
| **Admin** | `admin_manager` | Trần Quản Lý | ➡️ `/Admin/Dashboard` |
| **Admin** | `admin_tech` | Lê Kỹ Thuật | ➡️ `/Admin/Dashboard` |
| **User** | `user_an` | Nguyễn Văn An | ➡️ `/Product` (Cửa hàng mua sắm) |
| **User** | `user_bich` | Trần Thị Bích | ➡️ `/Product` |
| **User** | `user_cuong` | Lê Hoàng Cường | ➡️ `/Product` |
| ... | *7 tài khoản User khác* | *Đầy đủ thông tin* | ➡️ `/Product` |

*(Trên trang Đăng nhập `/Account/Login` có nút bấm điền tự động nhanh chóng để giáo viên/người chấm bài thử nghiệm ngay)*

---

## 📁 Cấu trúc thư mục mô hình MVC

```
examBlazor/
├── Controllers/
│   ├── HomeController.cs        # Trang chủ, giới thiệu
│   ├── ProductController.cs     # Danh sách sản phẩm, lọc theo danh mục, xem chi tiết
│   ├── AccountController.cs     # Đăng nhập, đăng ký, đăng xuất, phân quyền Admin vs User
│   ├── CartController.cs        # Quản lý giỏ hàng lưu bằng Session
│   └── AdminController.cs       # Quản trị hệ thống: Dashboard, CRUD sản phẩm, User list [Authorize(Roles = "Admin")]
├── Models/
│   ├── ApplicationUser.cs       # Kế thừa IdentityUser<int>
│   ├── ApplicationRole.cs       # Kế thừa IdentityRole<int>
│   ├── Category.cs              # Bảng danh mục sản phẩm (10 dòng)
│   ├── Product.cs               # Bảng sản phẩm (12 dòng)
│   └── ViewModels/
│       ├── LoginViewModel.cs    # Check validation đăng nhập
│       ├── RegisterViewModel.cs # Check validation đăng ký
│       ├── ProductViewModel.cs  # Check validation thêm/sửa sản phẩm
│       ├── CartItemViewModel.cs # Model giỏ hàng Session
│       └── DashboardViewModel.cs# Thống kê Admin
├── Data/
│   ├── AppDbContext.cs          # Kế thừa IdentityDbContext<ApplicationUser, ApplicationRole, int>
│   ├── DbInitializer.cs         # Khởi tạo tự động mỗi bảng >= 10 dòng
│   ├── AnnotationDbContext.cs   # Demo EF Core Annotation
│   ├── ConventionDbContext.cs   # Demo EF Core Convention
│   └── FluentApiDbContext.cs    # Demo EF Core Fluent API
├── Views/
│   ├── Shared/
│   │   ├── _Layout.cshtml       # Giao diện chính, Navbar động theo Role (Admin vs User)
│   │   └── _ValidationScriptsPartial.cshtml
│   ├── Home/                    # Index.cshtml, Privacy.cshtml
│   ├── Product/                 # Index.cshtml, Details.cshtml
│   ├── Account/                 # Login.cshtml, Register.cshtml, AccessDenied.cshtml
│   ├── Cart/                    # Index.cshtml, Checkout.cshtml, OrderSuccess.cshtml
│   └── Admin/                   # Dashboard.cshtml, Products.cshtml, CreateProduct.cshtml, EditProduct.cshtml, Users.cshtml
└── Program.cs                   # Pipeline MVC, Identity, Session, DbInitializer
```

---

## 🛠️ Hướng dẫn khởi chạy

Chạy lệnh sau tại thư mục dự án:
```bash
dotnet run
```
Truy cập trình duyệt theo URL hiển thị trên terminal (ví dụ: `http://localhost:5071` hoặc `https://localhost:7071`).
> **Tính năng tự động phục hồi kết nối**: Ứng dụng ưu tiên kết nối MySQL Database. Nếu Docker/MySQL chưa bật, hệ thống tự động kích hoạt SQLite (`shop_mvc.db`) nên luôn chạy mượt mà ngay lập tức.
