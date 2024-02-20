using DiabetesDiaryPages.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseSqlServer(connectionString));

var dataConnectionString = builder.Configuration.GetConnectionString("DataConnectionString") ?? throw new InvalidOperationException("Connection string 'DataConnectionString' not found.");
builder.Services.AddDbContext<DiabetesDiaryDbContext>(options =>
    options.UseNpgsql(dataConnectionString));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationIdentityDbContext>();

// Add DiabetesDiaryDbContext

builder.Services.AddDbContext<DiabetesDiaryDbContext>();

builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/RegisterPatient");
        options.Conventions.AllowAnonymousToAreaPage("Identity", "/Account/RegisterDoctor");
    });

builder.Services.Configure<IdentityOptions>(options =>
{
    // User settings.
    options.User.RequireUniqueEmail = true;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

var app = builder.Build();

// Add Doctor and Patient Roles to IdentityDB

using (var scope = app.Services.CreateAsyncScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ApplicationIdentityDbContext>();
    await context.Database.MigrateAsync();

    await SeedIdentityData.Initialize(services);
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
