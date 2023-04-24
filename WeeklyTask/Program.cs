using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Options;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Configuration;
using WeeklyTask.Areas.Identity.Data;
using WeeklyTask.Infrastructure.Components;
using WeeklyTask.Models.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Services;
using Microsoft.Extensions.Logging;


var builder = WebApplication.CreateBuilder(args);

//builder.Environment.EnvironmentName = "Development";
var connectionString = builder.Configuration.GetConnectionString("ApplicationDbContextConnection") ?? throw new InvalidOperationException("Connection string 'ApplicationDbContextConnection' not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Configure Stripe settings
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));
StripeConfiguration.ApiKey = "sk_test_51Mj6WQGhjWSbhfVTKWANfkM3tZT4k71D1cmYIF2w4SWjdnm610rCgXrhIuPIlQRK46UYXJcWWomIA6qtrXaHQ9I600nnWcLHW7";


// Add configuration sources
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var connectionString2 = builder.Configuration.GetConnectionString("FoodDbContextConnection") ?? throw new InvalidOperationException("Connection string 'FoodDbContextConnection' not found.");

builder.Services.AddMemoryCache();
builder.Services.AddTransient<SmallCartViewComponent>();
builder.Services.AddDbContext<FoodDbContext>(options =>
    options.UseSqlServer(connectionString2));
builder.Services.AddDistributedMemoryCache();
builder.Logging.AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Information);


// Add the OrderService
builder.Services.AddTransient<OrderService>();

builder.Services.AddLogging();


builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddSession(options =>
{
    options.Cookie.Name = ".MyApplication.Session";
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.IsEssential = true;
    options.Cookie.HttpOnly = true;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

/*// Add this block of code to apply migrations to the ApplicationDbContext
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// Add this block of code to apply migrations to the FoodDbContext
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FoodDbContext>();
    dbContext.Database.Migrate();
}*/

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();


app.UseAuthorization();

StripeConfiguration.ApiKey = "sk_test_51Mj6WQGhjWSbhfVTKWANfkM3tZT4k71D1cmYIF2w4SWjdnm610rCgXrhIuPIlQRK46UYXJcWWomIA6qtrXaHQ9I600nnWcLHW7";


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapRazorPages();

app.Run();
