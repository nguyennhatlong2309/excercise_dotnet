using examBlazor.Components;
using examBlazor.Data;
using examBlazor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var defaultConn = builder.Configuration.GetConnectionString("DefaultConnection")!;
bool isMySqlAvailable = false;
try
{
    using var testConn = new MySqlConnector.MySqlConnection(defaultConn);
    testConn.Open();
    isMySqlAvailable = true;
}
catch
{
    isMySqlAvailable = false;
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (isMySqlAvailable)
    {
        options.UseMySql(defaultConn, new MySqlServerVersion(new Version(8, 0, 36)));
    }
    else
    {
        options.UseSqlite("Data Source=shop_mvc.db");
    }
});

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromHours(8);
    options.SlidingExpiration = true;
    options.Cookie.HttpOnly = true;
    options.Cookie.SameSite = SameSiteMode.Lax;
});

var connConvention = builder.Configuration.GetConnectionString("ConventionDB")!;
builder.Services.AddDbContextFactory<ConventionDbContext>(options =>
{
    if (isMySqlAvailable) options.UseMySql(connConvention, new MySqlServerVersion(new Version(8, 0, 36)));
    else options.UseSqlite("Data Source=demo_convention.db");
});
builder.Services.AddScoped(sp => sp.GetRequiredService<IDbContextFactory<ConventionDbContext>>().CreateDbContext());

var connAnnotation = builder.Configuration.GetConnectionString("AnnotationDB")!;
builder.Services.AddDbContextFactory<AnnotationDbContext>(options =>
{
    if (isMySqlAvailable) options.UseMySql(connAnnotation, new MySqlServerVersion(new Version(8, 0, 36)));
    else options.UseSqlite("Data Source=demo_annotation.db");
});
builder.Services.AddScoped(sp => sp.GetRequiredService<IDbContextFactory<AnnotationDbContext>>().CreateDbContext());

var connFluent = builder.Configuration.GetConnectionString("FluentApiDB")!;
builder.Services.AddDbContextFactory<FluentApiDbContext>(options =>
{
    if (isMySqlAvailable) options.UseMySql(connFluent, new MySqlServerVersion(new Version(8, 0, 36)));
    else options.UseSqlite("Data Source=demo_fluent.db");
});
builder.Services.AddScoped(sp => sp.GetRequiredService<IDbContextFactory<FluentApiDbContext>>().CreateDbContext());

builder.Services.AddScoped<examBlazor.Services.ProductService>();
builder.Services.AddScoped<examBlazor.Services.CartService>();

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try { services.GetRequiredService<ConventionDbContext>().Database.EnsureCreated(); } catch { }
        try { services.GetRequiredService<AnnotationDbContext>().Database.EnsureCreated(); } catch { }
        try { services.GetRequiredService<FluentApiDbContext>().Database.EnsureCreated(); } catch { }
    }

    await DbInitializer.SeedDataAsync(app.Services);
}
catch { }

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
