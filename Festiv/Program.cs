using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Festiv.Data;
var builder = WebApplication.CreateBuilder(args);
   var connectionString = "server=localhost;user=festiv;password=festiv;database=festiv";
   var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

   builder.Services.AddDbContext<FestivDbContext>(dbContextOptions => dbContextOptions.UseMySql(connectionString, serverVersion));

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<FestivDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddDefaultIdentity<IdentityUser>
(options =>
{
   options.SignIn.RequireConfirmedAccount = true;
   options.Password.RequireDigit = false;
   options.Password.RequiredLength = 10;
   options.Password.RequireNonAlphanumeric = false;
   options.Password.RequireUppercase = true;
   options.Password.RequireLowercase = false;
}).AddEntityFrameworkStores<FestivDbContext>();

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
