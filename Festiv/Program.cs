using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Festiv.Data;
using Festiv.Models;
using Festiv.Services;
using Festiv.Controllers;
using Festiv.ViewModels;
using Microsoft.AspNetCore.Http;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;

var builder = WebApplication.CreateBuilder(args);

// Load configuation in appsettings.json
var configuration = builder.Configuration;

// MySql database configuation
   var connectionString = "server=localhost;user=festiv;password=festiv;database=festiv";
   var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

   builder.Services.AddDbContext<FestivDbContext>(dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion));
   builder.Services.AddControllersWithViews();
   builder.Services.AddIdentity<User, Role>(
    options => {   
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;

    })
        .AddEntityFrameworkStores<FestivDbContext>().AddDefaultTokenProviders();


        builder.Services.AddScoped<Festiv.Models.User>();
        builder.Services.AddScoped<IUserStore<User>, UserStore<User, Role, FestivDbContext, Guid>>();

    // Register UserManager and SignInManager
    builder.Services.AddScoped<UserManager<User>>();
    builder.Services.AddScoped<SignInManager<User>>();


// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();

// Spotify configuration
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => 
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddControllersWithViews();

// Register SpotifyService with HttpClient
builder.Services.AddHttpClient<SpotifyService>();
builder.Services.AddScoped<SpotifyService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseSession();

app.MapRazorPages();
app.MapControllers();

app.MapControllerRoute(
    name: "partyDetails",
    pattern: "PartyDetails/{partyId}",
    defaults: new { controller = "PartyDetails", action = "PartyDetails" });

app.MapControllerRoute(
    name: "spotify",
    pattern: "Spotify/{action}/{id?}",
    defaults: new { controller = "Spotify", action = "Index" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}",
    defaults: new { controller = "Home", action = "Index" });

app.Run();


