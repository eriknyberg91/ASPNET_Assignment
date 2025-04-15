using Data.Database;
using Data.Interfaces;
using Data.Repositories;
using Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("LocalDB")));

builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ProjectService>();

builder.Services.AddIdentity<AppUser, IdentityRole>(x =>
    {
        x.Password.RequiredLength = 8;
        x.User.RequireUniqueEmail = true;
        x.SignIn.RequireConfirmedAccount = false;
    })
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(x =>
{
    x.LoginPath = "/auth/signin";
    x.LogoutPath = "/auth/signout";
    x.AccessDeniedPath = "/auth/signin";
    x.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    x.SlidingExpiration = true;
});

builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseHsts();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=SignIn}/{id?}")
    .WithStaticAssets();
app.MapControllers();


app.Run();
