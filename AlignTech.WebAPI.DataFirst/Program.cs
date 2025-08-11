using AlignTech.WebAPI.DataFirst.CustomExceptions;
using AlignTech.WebAPI.DataFirst.Data;
using AlignTech.WebAPI.DataFirst.Interfaces;
using AlignTech.WebAPI.DataFirst.Mappings;
using AlignTech.WebAPI.DataFirst.Repositories;
using AlignTech.WebAPI.DataFirst.Services;
using AlignTech.WebAPI.DataFirst.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

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

//Register AutoMapper
builder.Services.AddAutoMapper(typeof(ProductProfiling));

//Register Validator
builder.Services.AddValidatorsFromAssemblyContaining<AddProductDtoValidator>();

//Register GlobalException Handler
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

//Authentication Scheme - JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["AppSettings:issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["AppSettings:audience"],
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["AppSettings:token"]!)),
        ValidateIssuerSigningKey = true
    };
});



//Register Serilog Configuration
Log.Logger = new LoggerConfiguration()
    // Add enrichers (additional context to every log entry)
    .Enrich.FromLogContext()        // Allows using LogContext.PushProperty()
    .Enrich.WithMachineName()	    // Add Machine Name
    .Enrich.WithThreadId()          // Adds thread ID
    .Enrich.WithEnvironmentName()   // Adds environment (Dev/Prod)

    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Async(wt => wt.File("Logs/log-.txt", rollingInterval: RollingInterval.Day))
    .WriteTo.Async(wt => wt.MSSqlServer(connectionString: builder.Configuration.GetConnectionString("MyConn"),
        sinkOptions: new Serilog.Sinks.MSSqlServer.MSSqlServerSinkOptions
        {
            TableName = "LogEvents",
            AutoCreateSqlTable = true
        }))
    .CreateLogger();

builder.Host.UseSerilog();

//Register Service and Repositories for JWT
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<ITokenService, TokenService>();

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

app.Run();
