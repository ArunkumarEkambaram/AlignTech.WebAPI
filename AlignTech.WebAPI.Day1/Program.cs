using AlignTech.WebAPI.Day1.Data;
using AlignTech.WebAPI.Day1.Interfaces;
using AlignTech.WebAPI.Day1.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Resolve DI
builder.Services.AddDbContext<MyStoreDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("MyStoreConnection")));

builder.Services.AddSingleton<IProductService, ProductService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers(); //Route

app.Run();
