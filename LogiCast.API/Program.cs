using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using LogiCast.Domain.DTOs;
using LogiCast.Domain.Interfaces;
using LogiCast.Infrastructure.Data;
using LogiCast.Infrastructure.Helpers;
using LogiCast.Infrastructure.Interfaces;
using LogiCast.Infrastructure.Repositories;
using LogiCast.Infrastructure.Services;
using LogiCast.Infrastructure.Validators;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// CORS setup
var corsPolicyName = "AllowReactApp";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        policy.WithOrigins("http://localhost:5173") // frontend origin
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddAuthentication(options =>
    {
        // This sets the default scheme for challenging (logging in) to Google
        options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
        // This sets the default scheme for authenticating subsequent requests (using the cookie we create)
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        // This sets the default scheme for signing in to Cookie
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    })
    .AddCookie(options => 
    {
        // Configure cookie options if needed (e.g., expiration, name)
        options.Cookie.SameSite = SameSiteMode.None; // Often needed for cross-site requests
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always; // Ensure secure cookie in production
        options.LoginPath = "/api/Auth/login"; // Optional: Path to redirect to for login
        options.LogoutPath = "/api/Auth/logout"; // Optional: Path for logout
    })
    .AddGoogle(options =>
    {
        // Read configuration from appsettings.json
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
        
        // Optional: If you want to request specific scopes (permissions) beyond the default profile & email
        // options.Scope.Add("https://www.googleapis.com/auth/...");
        
        // Optional: Save the raw access and refresh tokens to the HttpContext
        // options.SaveTokens = true;
        
        // Optional: Change the default callback path if needed. If you change it, you MUST also change it in Google Cloud Console.
        // options.CallbackPath = "/my-custom-auth-callback"; 
    });

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(cfg => 
{
    cfg.AddMaps(typeof(AutoMapperProfile));
});

// Add services to the container.
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IItemRepositroy, ItemRepository>();
builder.Services.AddScoped<IItemService, ItemService>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IInventoryReportService, InventoryReportService>();

builder.Services.AddScoped<IValidator<CreateItemDto>, CreateItemValidator>();
builder.Services.AddScoped<IValidator<CreateWarehouseDto>, CreateWarehouseValidator>();
builder.Services.AddScoped<IValidator<CreateInventoryDto>, CreateInventoryValidator>();

QuestPDF.Settings.License = LicenseType.Community;

// Add other necessary services

builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<CreateItemValidator>();
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddControllers()
//    .AddJsonOptions(options =>
//    {
//        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
//    });
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
