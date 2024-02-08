using SocializR.Services.MediaServices;

namespace SocializR.SPA.Server.Configuration.ExtensionMethods;

public static class ConfigurationExtensionMethods
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IHostEnvironment env)
    {
        services.AddScoped<ApplicationUnitOfWork>();
        services.ConfigureServices();
        services.AddScoped<IValidationService, ValidationService>();
        services.AddScoped<TokenService>();

        if (env.IsProduction())
        {
            services.AddScoped<IImageStorage, AzureStorageService>();
        }
        else
        {
            services.AddScoped<IImageStorage, LocalStorageService>();
        }

        return services;
    }

    public static IServiceCollection AddCurrentUser(this IServiceCollection services)
    {
        services.AddScoped(serviceProvider =>
        {
            var contextAccessor = serviceProvider.GetService<IHttpContextAccessor>();
            var context = contextAccessor.HttpContext;
            var id = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

            if (id != string.Empty)
            {
                return new CurrentUser(isAuthenticated: true) { Id = new Guid(id) };
            }
            else
            {
                return new CurrentUser(isAuthenticated: false);
            }
        });

        return services;
    }
}
