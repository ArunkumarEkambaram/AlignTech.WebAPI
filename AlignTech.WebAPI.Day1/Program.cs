using AlignTech.WebAPI.Day1.Interfaces;
using AlignTech.WebAPI.Day1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Resolve DI
builder.Services.AddSingleton<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/Greet", ()=>"Hello World!");

app.Use((context, next) =>
{
    app.Logger.LogInformation($"Request Path :{context.Request.Path}");
    return next();
});

app.UseHttpsRedirection();

//app.UseRouting();

//app.UseAuthorization();

app.MapControllers(); //Route

app.Run();
