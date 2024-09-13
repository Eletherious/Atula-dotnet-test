using AtulaTestWebsite.Data;
using AtulaTestWebsite.DataAccess.IRepository;
using AtulaTestWebsite.DataAccess.Repository;
using AtulaTestWebsite.Models.Modles;
using AtulaTestWebsite.Models.Validators;
using AtulaTestWebsite.Models.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

MappingConfig.RegisterMappings();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IValidator<Product>, ProductValidator>();
builder.Services.AddTransient<IValidator<ProductVM>, ProductVMValidator>();
builder.Services.AddTransient<IValidator<Category>, CategoryValidator>();
builder.Services.AddTransient<IValidator<CategoryVM>, CategoryVMValidator>();
builder.Services.AddTransient<IValidator<ApplicationUser>, ApplicationUserValidator>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddRazorPages();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
