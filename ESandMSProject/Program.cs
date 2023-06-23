using ESandMSProject.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
   options.UseSqlServer(builder.Configuration
   .GetConnectionString("DbConnectionString")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.ExpireTimeSpan = TimeSpan.FromDays(1);
		options.SlidingExpiration = true;
		options.LoginPath = "/Logins/Index"; // This setting determines the page the user will be redirected to before authenticating.
        options.LogoutPath = "/Logins/Logout"; //This setting determines the URL from which the user logs out.
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Adds the authentication middleware to the pipeline.

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Logins}/{action=Index}");

app.Run();
