using Asp.Versioning;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using WM.Application;
using WM.Persistence;

namespace WM.API;

public class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();

        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;

            options.ApiVersionReader = ApiVersionReader.Combine(
                //new UrlSegmentApiVersionReader(),
                //new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("X-API-Version")
            );
        })
           .AddMvc();


        #region qwenAI swagger

        // Настройка Swagger/OpenAPI
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            // Определяем документы для каждой версии
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "My API",
                Description = "An API with versioning via headers"
            });

            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "My API",
                Description = "An API with versioning via headers"
            });

            // Автоматически подключаем только те методы, которые соответствуют версии
            options.DocInclusionPredicate((docName, apiDesc) =>
            {
                if (!apiDesc.TryGetMethodInfo(out var methodInfo)) return false;

                var versions = methodInfo.DeclaringType?
                    .GetCustomAttributes(inherit: true)
                    .OfType<ApiVersionAttribute>()
                    .SelectMany(attr => attr.Versions);

                return versions?.Any(v => $"v{v}" == docName) ?? false;
            });

            // Добавляем версию в операции
            options.CustomOperationIds(apiDesc =>
            {
                if (apiDesc.TryGetMethodInfo(out var methodInfo))
                {
                    return $"{methodInfo.DeclaringType?.Name}_{methodInfo.Name}";
                }
                return null;
            });
        });

        #endregion

        builder.Services.AddCors(options =>
                 options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
             );

        builder.Services.ConfigureApplicationServices();
        builder.Services.ConfigurePersistenceServices(builder.Configuration);

        var app = builder.Build();

        app.UseCors("AllowAll");
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "v2");
            });
        }

        app.MapGet("/", () => "VM started...");
        app.MapControllers();
        app.Run();
    }
}