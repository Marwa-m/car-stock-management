using CarDealerDemo.Database;
using CarDealerDemo.Factory;
using CarDealerDemo.Helper;
using CarDealerDemo.Services;

namespace CarDealerDemo.Configuration;

public static class ServiceRegistration
{
    public static IServiceCollection AddServicesRegistration(this IServiceCollection services)
    {
        services.AddSingleton<ISqliteConnectionFactory, SqliteConnectionFactory>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
        services.AddScoped<ICarRepository, CarRepository>();
        services.AddScoped<IDealerRepository, DealerRepository>();
        services.AddScoped<IDealerService, DealerService>();
        services.AddScoped<ICarService, CarService>();
        services.AddScoped<IJWTService, JWTService>();
        return services;
    }
}
