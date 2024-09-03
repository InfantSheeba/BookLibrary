//using Microsoft.AspNetCore.Builder;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Hosting;
//using System.Data;
//using System.Data.SqlClient;
//using BookLibraryManagementSystem.Data;
//using BookLibraryManagementSystem.Controllers;
//using BookLibraryManagementSystem.Helpers;

//var builder = WebApplication.CreateBuilder(args);

//// Register services
//builder.Services.AddControllersWithViews();
//// Register PasswordHelper as a service
//builder.Services.AddSingleton<PasswordHelper>();



//// Register UserRepository with IDbConnection
//builder.Services.AddTransient<IDbConnection>(provider => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddTransient<IUserRepository, UserRepository>();

//// Add session services
//builder.Services.AddDistributedMemoryCache();
//builder.Services.AddSession(options =>
//{
//    options.IdleTimeout = TimeSpan.FromMinutes(30);
//    options.Cookie.HttpOnly = true;
//    options.Cookie.IsEssential = true;
//});

//var app = builder.Build();

//// Configure middleware
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();
//app.UseRouting();
//app.UseSession(); // Use session middleware
//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=LoginAccount}/{action=Login}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=RegisterAccount}/{action=Register}/{id?}");

//app.Run();



using BookLibraryManagementSystem.Data;
using System.Data;
using System.Data.SqlClient; // Ensure you use the correct database provider

var builder = WebApplication.CreateBuilder(args);

// Register the connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register IDbConnection with SqlConnection (or another provider) using the connection string
builder.Services.AddTransient<IDbConnection>(provider => new SqlConnection(connectionString));

// Register UserRepository with IDbConnection
builder.Services.AddTransient<IUserRepository, UserRepository>();

// Register other services, e.g., controllers with views
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=RegisterAccount}/{action=Register}/{id?}");

app.Run();
