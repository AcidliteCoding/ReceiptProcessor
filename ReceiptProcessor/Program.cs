using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using ReceiptProcessor.Services;
using Microsoft.Extensions.Hosting; // For checking environment (Development, Production)

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register Created Service
builder.Services.AddScoped<ReceiptService>();
builder.Services.AddEndpointsApiExplorer(); // For Swagger
builder.Services.AddSwaggerGen(); // Add Swagger generator


var app = builder.Build();

// Enable Swagger middleware in the development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Generates Swagger JSON
    app.UseSwaggerUI(); // Provides an interactive UI
}

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


/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
*/

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Receipts}/{action=ProcessReceipt}/");
//pattern: "{controller=Receipts}/{action=ProcessReceipt}/{id?}");

app.Run();
