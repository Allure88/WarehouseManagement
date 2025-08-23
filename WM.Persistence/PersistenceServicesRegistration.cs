using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using WM.Application.Contracts;
using WM.Persistence.Repositories;

namespace WM.Persistence;

public static class PersistenceServicesRegistration
{
    public static void ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        string connString = configuration.GetConnectionString("PostgresWmDbConnectionString")!;

        services.AddDbContext<WmDbContext>(options =>
                options.UseNpgsql(connString
                   //,npgsqlOptions => npgsqlOptions.EnableRetryOnFailure(
                   // maxRetryCount: 5,
                   // maxRetryDelay: TimeSpan.FromSeconds(30),
                   // errorCodesToAdd: ["08006", "08001", "08004"] // network, connection errors
              //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);  
        ));

        services.AddScoped<IUnitsRepository, UnitsRepository>(); //
        services.AddScoped<IResourceRepository, ResourceRepository>(); //
        services.AddScoped<IClientRepository, ClientRepository>();//
        services.AddScoped<IBalanceRepository, BalanceRepository>();//
        services.AddScoped<IAdmissionDocRepository, AdmissionDocRepository>();
        services.AddScoped<IAdmissionResRepository, AdmissionResRepository>();
        services.AddScoped<IShippingDocRepository, ShippingDocRepository>();
        services.AddScoped<IShippingResRepository, ShippingResRepository>();

        //для отладки
        //return connString;
    }
}
