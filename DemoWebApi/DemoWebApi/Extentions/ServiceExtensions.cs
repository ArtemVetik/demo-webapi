using Contracts;
using Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Serilog;
using Service;
using System.Text;

namespace DemoWebApi.Extentions
{
    public static class ServiceExtensions
    {
        public static void AddSerilog(this IServiceCollection services, ILoggingBuilder loggingBuilder, IWebHostEnvironment environment)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .MinimumLevel.Is(environment.IsProduction() ? Serilog.Events.LogEventLevel.Information : Serilog.Events.LogEventLevel.Debug)
                .CreateLogger();

            loggingBuilder.ClearProviders();

            if (environment.IsProduction() == false)
            {
                loggingBuilder.AddSerilog();
                services.AddHttpLogging(o =>
                {
                    o.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All;
                });
            }
        }

        public static void ConfigureSwager(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API - V1", Version = "v1" });
            });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigurePostgreSqlContext(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["PostgresqlConnection:ConnectionString"];
            services.AddDbContext<RepositoryContext>(options => options.UseNpgsql(connectionString));
        }

        public static void ConfigureRepositoryWrapper(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        public static void ConfigureJwtConfig(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<JwtConfig>(config.GetSection("JwtConfig"));
        }

        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = true;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JwtConfig:secret"]))
                };
            });
        }
    }
}
