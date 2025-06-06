using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PersonalFinanceManager.Application.Constants;
using PersonalFinanceManager.Application.Database;
using PersonalFinanceManager.Application.Models;
using PersonalFinanceManager.Application.Services;
using PersonalFinanceManager.Application.Services.IServices;
using System.Security.Claims;
using System.Text;

namespace PersonalFinanceManager.Application;

public static class ApplicationServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration config)
    {
        services.AddValidatorsFromAssemblyContaining<IApplicationMarker>(ServiceLifetime.Singleton);
        services.AddAuth(config);
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IDbConnectionFactory>(new NpgSqlConnectionFactory(connectionString));
        services.AddSingleton<DBInitializer>();

        services.AddDbContext<AppDbContext>(options =>
                          options.UseNpgsql(connectionString));
        return services;
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
    {
         services.AddAuthorization();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }
         )
           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.RequireHttpsMetadata = false;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                   ValidAudience = config["JWT:ValidAudience"],
                   ValidIssuer = config["JWT:ValidIssuer"],
                   ClockSkew = TimeSpan.Zero,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:secret"]!))
               };
           }
            );

        services.AddIdentity<ApplicationUser, IdentityRole>()
                        .AddEntityFrameworkStores<AppDbContext>()
                        .AddDefaultTokenProviders();


        //services.AddAuthorization(x =>
        //{
        //    x.AddPolicy(AuthConstants.UserPolicyName,
        //p => p.RequireAssertion(c =>
        //    c.User.HasClaim(m => m is { Type: ClaimTypes.Role, Value: Roles.User }) ||
        //    c.User.HasClaim(m => m is { Type: ClaimTypes.Role, Value: Roles.Admin })));
        //});

        return services;

    }
}
