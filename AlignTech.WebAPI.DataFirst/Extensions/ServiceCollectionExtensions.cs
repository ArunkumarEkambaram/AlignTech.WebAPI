using AlignTech.WebAPI.DataFirst.CustomExceptions;
using AlignTech.WebAPI.DataFirst.Data;
using AlignTech.WebAPI.DataFirst.Filters;
using AlignTech.WebAPI.DataFirst.Interfaces;
using AlignTech.WebAPI.DataFirst.Mappings;
using AlignTech.WebAPI.DataFirst.Repositories;
using AlignTech.WebAPI.DataFirst.Services;
using AlignTech.WebAPI.DataFirst.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AlignTech.WebAPI.DataFirst.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Resolve DbContext
            services.AddDbContext<QuickKartDbContext>(opt => opt.UseSqlServer(configuration.GetConnectionString("MyConn")));

            //Resolve Product Repository
            services.AddScoped<IProductRepository, ProductRepository>();

            //Relsove Product Service
            services.AddScoped<IProductService, ProductService>();

            //Resolve Service and Repositories for JWT
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ITokenService, TokenService>();

            //Register AutoMapper
            services.AddAutoMapper(typeof(ProductProfiling));

            //Register Validator
            services.AddValidatorsFromAssemblyContaining<AddProductDtoValidator>();

            //Register GlobalException Handler
            services.AddExceptionHandler<GlobalExceptionHandler>();
            services.AddProblemDetails();

            //Register Subscription Filter
            services.AddScoped<MySubscriptionFilter>();
            //Register Action Filter
            services.AddScoped<PerformanceActionFilter>();
            //Register Resource Filter
            services.AddScoped<IpWishListResourceFilter>(opt =>
            {
                var allowedIps = new[] { "192.168.1.136", "192.168.1.135", "::1" };
                var service = opt.GetRequiredService<ILogger<IpWishListResourceFilter>>();
                return new IpWishListResourceFilter(service, allowedIps);
            });            

            //Authentication Scheme - JWT
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["AppSettings:issuer"],
                    ValidateAudience = true,
                    ValidAudience = configuration["AppSettings:audience"],
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AppSettings:token"]!)),
                    ValidateIssuerSigningKey = true
                };
            });


            return services;
        }
    }
}
