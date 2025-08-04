using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WM.Application.Contracts;
using WM.Persistence.Repositories;

namespace WM.Persistence;

public static class PersistenceServicesRegistration
{
    public static void ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        string connString = configuration.GetConnectionString("PostgresWmDbConnectionString")!;

        services.AddDbContext<WmDbContext>(options =>
        {
            options.UseNpgsql(connString);

            //options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);  
        });

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
