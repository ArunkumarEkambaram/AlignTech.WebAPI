using AlignTech.WebAPI.DataFirst.Data;
using AlignTech.WebAPI.DataFirst.Interfaces;
using AlignTech.WebAPI.DataFirst.Mappings;
using AlignTech.WebAPI.DataFirst.Repositories;
using AlignTech.WebAPI.DataFirst.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Resolve DbContext
builder.Services.AddDbContext<QuickKartDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("MyConn")));

//Resolve Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//Relsove Service
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAutoMapper(typeof(ProductProfiling));

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
