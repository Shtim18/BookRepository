using BookLibrary.Data;
using BookLibrary.Repositories;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<BookRepository>();

builder.Services.AddDbContext<ApplicationDatabaseContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});


builder.Services.AddControllersWithViews();
builder.Services.AddTransient<FileUploadService>();

var app = builder.Build();

TestData.TData(app);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
  pattern: "{controller=Book}/{action=Books}/{id?}");

app.Run();
