
using DiplomaSite3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DiplomaSite3.Models;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
AddServices(builder.Services);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<DiplomaSite3Context>();
        var userManager = services.GetRequiredService<UserManager<UserModel>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        SeedDB.Initialize(services);
        await SeedDB.SeedRolesAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occurred seeding the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();

void AddServices(IServiceCollection services)
{
    services.AddDbContext<DiplomaSite3Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DiplomaSite3Context") ?? throw new InvalidOperationException("Connection string 'DiplomaSite3Context' not found.")));

    services.AddDefaultIdentity<UserModel>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<DiplomaSite3Context>();

    services
     .AddAuthorization().AddAuthentication();

    // password hashing
    services.AddScoped<IPasswordHasher<UserModel>, MyPassHashing>();

    services.AddControllersWithViews();
    services.AddRazorPages();
    services.ConfigureApplicationCookie(options =>
    {
        // Cookie settings
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

        options.LoginPath = "/Identity/Account/Login";
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        options.SlidingExpiration = true;
    });
}