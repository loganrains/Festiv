using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Festiv.Data;
using Festiv.Models;
using Festiv.Controllers;
using Festiv.ViewModels;
using Microsoft.AspNetCore.Http;
var builder = WebApplication.CreateBuilder(args);
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

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpContextAccessor();


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
