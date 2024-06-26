using Microsoft.EntityFrameworkCore;
using ClothesStore.Data;
using ClothesStore.Services;
using ClothesStore.Services.implements;
Console.WriteLine("Program.cs");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ClothesStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClothesStoreContext") ?? throw new InvalidOperationException("Connection string 'ClothesStoreContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IUploader, UploaderLocal>();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

