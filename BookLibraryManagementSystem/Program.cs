using BookLibraryManagementSystem.Data;

var builder = WebApplication.CreateBuilder(args);

// Register the connection string as an option or directly pass it
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register UserRepository with the connection string
builder.Services.AddTransient<UserRepository>(provider => new UserRepository(connectionString));

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
    pattern: "{controller=Account}/{action=Register}/{id?}");

app.Run();
