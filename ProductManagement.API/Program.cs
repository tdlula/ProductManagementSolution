// Program.cs - Entry point for ProductManagement.API
// Configures services, middleware, logging, validation, and error handling for the API.

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Interfaces;
using ProductManagement.Infrastructure.Repositories;
using ProductManager.Application.Interfaces;
using ProductManager.Application.Services;
using Serilog;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configure Serilog for structured logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Host.UseSerilog();

// Add controllers and FluentValidation integration
builder.Services.AddControllers();
// Register FluentValidation for automatic model validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ProductManagement.API.Validators.ProductValidator>();

// Register Entity Framework Core with InMemory database
builder.Services.AddDbContext<ProductContext>(options =>
    options.UseInMemoryDatabase("ProductsDb")); // Use InMemory DB for simplicity

// Register repository and service dependencies
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();

// Register OpenAPI/Swagger for API documentation
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Product Management API",
        Version = "v1",
        Description = "RESTful API using ASP.NET Core that manages a collection of Products. The API supports operations to create, read, update, and delete products. Each product has properties like ProductID, Name, Description, and Price.\n\n" +
                      "A modular .NET 9 solution for managing products, following clean architecture principles.\n\n" +
                      "**Features:**\n" +
                      "- Clean separation of concerns (API, Application, Infrastructure, Domain)\n" +
                      "- Dependency Injection throughout\n" +
                      "- DTOs for API requests/responses\n" +
                      "- FluentValidation for model validation\n" +
                      "- Serilog for structured logging\n" +
                      "- Global exception handling middleware\n" +
                      "- OpenAPI/Swagger for API documentation\n" +
                      "- CORS enabled for development\n" +
                      "- Comprehensive unit and integration tests"
    });
});

// Register CORS policy to allow all origins, methods, and headers
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Add global exception handling middleware (should be first in the pipeline)
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        // Log unhandled exceptions with Serilog
        Log.Error(ex, "Unhandled exception for {Path}", context.Request.Path);
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";
        await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred.", traceId = context.TraceIdentifier });
    }
});

// Add Serilog request logging middleware
app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();

// Redirect root URL to Swagger UI
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/" || context.Request.Path == "/index.html")
    {
        context.Response.Redirect("/swagger/index.html");
        return;
    }
    await next();
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
