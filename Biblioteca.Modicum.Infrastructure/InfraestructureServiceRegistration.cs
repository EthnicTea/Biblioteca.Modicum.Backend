using Biblioteca.Modicum.Application.Configurations;
using Biblioteca.Modicum.Application.Feature.Interfaces.Identity;
using Biblioteca.Modicum.Application.Feature.Interfaces.Libro;
using Biblioteca.Modicum.Application.Feature.Interfaces.Prestamo;
using Biblioteca.Modicum.Domain.Entities;
using Biblioteca.Modicum.Infrastructure.Queries;
using Biblioteca.Modicum.Infrastructure.Repositories;
using Biblioteca.Modicum.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Npgsql;
using System.Data;
using System.Text;

namespace Biblioteca.Modicum.Infrastructure;

public static class InfraestructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        string source = configuration.GetSection("ConnectionStrings")["ModicumDb"] ?? "";

        services.AddDbContext<ModicumDbContext>(options =>
            options.UseNpgsql(source));

        services.AddTransient<IDbConnection>((sp) => new NpgsqlConnection(source));

        services.AddScoped<ILibroQueries, LibroQueries>();
        services.AddScoped<ILibroRepositories, LibroRepositories>();

        services.AddScoped<IPrestamoQueries, PrestamoQueries>();
        services.AddScoped<IPrestamoRepositories, PrestamoRepositories>();

        services.AddScoped<IAuthService, AuthService>();

        // MICROSOFT IDENTITY
        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<ModicumDbContext>()
        .AddDefaultTokenProviders();

        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = false;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = jwtSettings?.Issuer,
                ValidAudience = jwtSettings?.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings!.Key))
            };
        });
        return services;
    }
}