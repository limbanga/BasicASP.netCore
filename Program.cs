using Microsoft.EntityFrameworkCore;
using ClothesStore.Data;
using ClothesStore.Services;
using ClothesStore.Services.implements;
using Microsoft.AspNetCore.Identity;
using ClothesStore.Models;
using Microsoft.AspNetCore.Authorization;
using ClothesStore.Repositories;
using ClothesStore.Repositories.implements;
Console.WriteLine("Program.cs");

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ClothesStoreContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ClothesStoreContext") ?? throw new InvalidOperationException("Connection string 'ClothesStoreContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IUploader, UploaderLocal>();
builder.Services.AddTransient<IAuthRepository, AuthRepository>();
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<ApplicationUserz>()
    .AddEntityFrameworkStores<ClothesStoreContext>();
builder.Services.ConfigureApplicationCookie(o => {
    o.ExpireTimeSpan = TimeSpan.FromDays(5);
    o.SlidingExpiration = true;
});

builder.Services.Configure<DataProtectionTokenProviderOptions>(o =>
       o.TokenLifespan = TimeSpan.FromHours(3));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.MapIdentityApi<ApplicationUserz>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

