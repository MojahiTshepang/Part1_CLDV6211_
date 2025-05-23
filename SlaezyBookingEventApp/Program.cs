using Microsoft.EntityFrameworkCore;
using SlaezyBookingEventApp.Data;
using SlaezyBookingEventApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IImageService, AzureBlobImageService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add the DbContext registration here:
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IImageService, AzureBlobImageService>();

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

