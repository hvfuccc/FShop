using FShop.Data.Entities;
using FShop.Data.EntityFramework;
using FShop.Service.Images;
using FShop.Service.Images.Impl;
using FShop.Service.Products;
using FShop.Service.Products.Impl;
using FShop.Service.Users;
using FShop.Service.Users.Impl;
using FShop.Utilities.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString(SystemConstant.ConnectionString);
builder.Services.AddDbContext<FShopDBContext>(option =>
                        option.UseSqlServer(connectionString));
builder.Services.AddIdentity<User, Role>()
    .AddEntityFrameworkStores<FShopDBContext>()
    .AddDefaultTokenProviders();

builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IAdminProductService, AdminProductService>();
builder.Services.AddTransient<IStorageService, StorageService>();
builder.Services.AddTransient<UserManager<User>, UserManager<User>>();
builder.Services.AddTransient<SignInManager<User>, SignInManager<User>>();
builder.Services.AddTransient<RoleManager<Role>, RoleManager<Role>>();
builder.Services.AddTransient<IUserService, UserService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ToDo API",
        Description = "FShop Web API for managing ToDo items"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger FShop V1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin();
    builder.AllowAnyMethod();
    builder.AllowAnyHeader();
});

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();