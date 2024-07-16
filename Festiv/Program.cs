using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Festiv.Data;
using Festiv.Models;
using Festiv.Services;
using Festiv.Controllers;
using Festiv.ViewModels;
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

//    Identity configuration
   builder.Services.AddIdentity<User, IdentityRole>(
    options => {   
        options.Password.RequiredUniqueChars = 0;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireLowercase = false;

    })
        .AddEntityFrameworkStores<FestivDbContext>().AddDefaultTokenProviders();


        builder.Services.AddScoped<Festiv.Models.User>();
//    builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true)
//     .AddEntityFrameworkStores<FestivDbContext>();

// builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<FestivDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Spotify configuration
var spotifySettings = configuration.GetSection("Spotify").Get<SpotifySettings>();

builder.Services.AddSingleton<ISpotifyClient>(ServiceProvider =>
{
    var config = SpotifyClientConfig.CreateDefault()
        .WithAuthenticator(new ClientCredentialsAuthenticator(spotifySettings.ClientId, spotifySettings.ClientSecret));
    return new SpotifyClient(config);
});

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

app.UseAuthorization();
app.MapRazorPages();
app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


