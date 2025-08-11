using Microsoft.EntityFrameworkCore;
using PAW.Business;
using PAW.Data.MSSql;
using PAW.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepositoryCatalogTask, RepositoryCatalogTask>();

// Repositories
builder.Services.AddScoped<IRepositoryProduct, RepositoryProduct>();
builder.Services.AddScoped<IRepositoryCategory, RepositoryCategory>();
builder.Services.AddScoped<IRepositoryCatalog, RepositoryCatalog>();
builder.Services.AddScoped<IRepositorySupplier, RepositorySupplier>();
builder.Services.AddScoped<IRepositoryInventory, RepositoryInventory>();
builder.Services.AddScoped<IRepositoryNotification, RepositoryNotification>();
builder.Services.AddScoped<IRepositoryUser, RepositoryUser>();
builder.Services.AddScoped<IRepositoryUserRole, RepositoryUserRole>();

// Business
builder.Services.AddScoped<IBusinessProduct, BusinessProduct>();
builder.Services.AddScoped<IBusinessCategory, BusinessCategory>();
builder.Services.AddScoped<IBusinessCatalog, BusinessCatalog>();
builder.Services.AddScoped<IBusinessSupplier, BusinessSupplier>();
builder.Services.AddScoped<IBusinessInventory, BusinessInventory>();
builder.Services.AddScoped<IBusinessNotification, BusinessNotification>();
builder.Services.AddScoped<IBusinessUser, BusinessUser>();
builder.Services.AddScoped<IBusinessUserRole, BusinessUserRole>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
