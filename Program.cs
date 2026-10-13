using examBlazor.Components;
using examBlazor.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// =====================================================================
// ĐĂNG KÝ 3 DBCONTEXT — DEMO 3 CÁCH CẤU HÌNH EF CORE (THEO SHOPINTERFACE1)
// =====================================================================

// CÁCH 1: CONVENTION — EF tự đọc quy ước đặt tên (ShopDB_Convention)
var connConvention = builder.Configuration.GetConnectionString("ConventionDB")!;
builder.Services.AddDbContextFactory<ConventionDbContext>(options =>
    options.UseMySql(connConvention, ServerVersion.AutoDetect(connConvention)));
builder.Services.AddScoped(sp => sp.GetRequiredService<IDbContextFactory<ConventionDbContext>>().CreateDbContext());

// CÁCH 2: DATA ANNOTATION — Attribute trên model class (ShopDB_Annotation)
var connAnnotation = builder.Configuration.GetConnectionString("AnnotationDB")!;
builder.Services.AddDbContextFactory<AnnotationDbContext>(options =>
    options.UseMySql(connAnnotation, ServerVersion.AutoDetect(connAnnotation)));
builder.Services.AddScoped(sp => sp.GetRequiredService<IDbContextFactory<AnnotationDbContext>>().CreateDbContext());

// CÁCH 3: FLUENT API — Cấu hình trong OnModelCreating() (ShopDB_FluentApi)
var connFluent = builder.Configuration.GetConnectionString("FluentApiDB")!;
builder.Services.AddDbContextFactory<FluentApiDbContext>(options =>
    options.UseMySql(connFluent, ServerVersion.AutoDetect(connFluent)));
builder.Services.AddScoped(sp => sp.GetRequiredService<IDbContextFactory<FluentApiDbContext>>().CreateDbContext());

// Services cho cửa hàng
builder.Services.AddScoped<examBlazor.Services.ProductService>();
builder.Services.AddScoped<examBlazor.Services.CartService>();

var app = builder.Build();

// =====================================================================
// TỰ ĐỘNG TẠO 3 DATABASE & BẢNG KHI KHỞI ĐỘNG (CODE-FIRST)
// =====================================================================
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    services.GetRequiredService<ConventionDbContext>().Database.EnsureCreated();
    services.GetRequiredService<AnnotationDbContext>().Database.EnsureCreated();
    services.GetRequiredService<FluentApiDbContext>().Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
