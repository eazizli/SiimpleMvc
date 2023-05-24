using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SiimpleMvc.DataContex;
using SiimpleMvc.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<SiimpleDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 8;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireUppercase = false;  
}).AddEntityFrameworkStores<SiimpleDbContext>().AddDefaultTokenProviders();
var app = builder.Build();

app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
          );
app.UseAuthentication();
app.UseAuthorization();
app.MapDefaultControllerRoute();
app.UseStaticFiles();
app.Run();
