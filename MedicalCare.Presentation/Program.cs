using MedicalCare.Application.Features.TestCategories;
using MedicalCare.Application.Features.Tests;
using MedicalCare.Infrastructure;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddScoped<CreateTestCategoryHandler>();
builder.Services.AddScoped<UpdateTestCategoryHandler>();
builder.Services.AddScoped<GetAllTestCategoriesHandler>();
builder.Services.AddScoped<ToggleTestCategoryStatusHandler>();
builder.Services.AddScoped<CreateTestHandler>();
builder.Services.AddScoped<UpdateTestHandler>();
builder.Services.AddScoped<GetAllTestsHandler>();
builder.Services.AddScoped<ToggleTestStatusHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

// Add area route so controllers marked with [Area("Admin")] can be reached at /Admin/{controller}/{action}
//app.MapControllerRoute(
//    name: "admin",
//    pattern: "admin/{controller=Home}/{action=Index}/{id?}")
//    .WithStaticAssets();


//// Default route for non-area controllers (e.g. WebsiteHomeController)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=WebsiteHome}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
