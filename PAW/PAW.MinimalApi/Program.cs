using PAW.Business;
using PAW.MinimalApi.Factory;
using PAW.Models;
using PAW.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICatalogFactory, CatalogFactory>();
builder.Services.AddScoped<IBusinessCatalog, BusinessCatalog>();
builder.Services.AddScoped<IRepositoryCatalog, RepositoryCatalog>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/catalog", async (ICatalogFactory factory, IBusinessCatalog business) =>
{
    var catalog = factory.CreateEntity<Catalog>() as Catalog;
    await business.SaveCatalogAsync(catalog);
});

app.MapPut("/catalog", async (Catalog? catalog, ICatalogFactory factory, IBusinessCatalog business) =>
{
    catalog ??= factory.CreateEntity<Catalog>() as Catalog;
    factory.ApplyAuditing(catalog, string.Empty, isInserting: false);
    await business.SaveCatalogAsync(catalog);
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
