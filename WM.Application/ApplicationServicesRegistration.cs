using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using WM.Application.Mapper_Profiles;

namespace WM.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        services.AddAutoMapper(cfg =>
        {
            cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
            cfg.AddProfile<MainProfile>();
        });

        return services;
    }
}
