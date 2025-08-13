using AlignTech.WebAPI.DataFirst.Extensions;
using AlignTech.WebAPI.DataFirst.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    //options.Filters.AddService<MySubscriptionFilter>();
    //options.Filters.AddService<PerformanceActionFilter>();
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//Register all services
builder.Services.AddApplicationServices(builder.Configuration);

//Register Serilog Extension
builder.ConfigureSerilog();

//Configure CORS Policy
builder.Services.AddCors(options =>
{
    //MVC
    options.AddPolicy("MVCApp", option =>
    {
        //option.AllowAnyOrigin();
        option.WithOrigins("https://localhost:7119");
        option.AllowAnyHeader();
        option.AllowAnyMethod();
    });

    //Angular
    options.AddPolicy("AngularApp", opt =>
    {
        opt.WithOrigins("https://localhost:7200/");
        opt.AllowAnyHeader();
        opt.WithMethods("GET");
    });
});



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//Custom Exception Handler
app.UseExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("MVCApp");
//app.UseCors("AngularApp");

app.Run();
