using WebUI.Controllers;
using Domain.Abstract;
using Domain.Concrete;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvcCore();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();  // добавляем сервисы сессии

// получаем строку подключения из файла конфигурации
string connection = builder.Configuration.GetConnectionString("DefaultConnection");
// добавляем контекст EFDbContext в качестве сервиса в приложение
builder.Services.AddDbContext<EFDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddTransient<IBookRepository, EFBookRepository>();

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

app.UseSession();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Books}/{action=List}/{id?}");

app.MapControllerRoute(
    name: "genreFilter",
    pattern: "{controller=Books}/{action=List}/{genre?}",
    defaults: new { controller = "Books", action = "List"}
    );



app.Run();
