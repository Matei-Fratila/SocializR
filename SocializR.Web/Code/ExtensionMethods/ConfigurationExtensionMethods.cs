using SocializR.Services.Configuration;

namespace SocializR.Web.Code.ExtensionMethods;

public static class ConfigurationExtensionMethods
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IHostEnvironment env)
    {
        services.ConfigureServices();
        services.AddScoped<IValidationService, ValidationService>();

        if (env.IsProduction())
        {
            //services.AddScoped<IImageStorage, AzureStorageService>();
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
            var mail = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value ?? string.Empty;
            var _accountService= serviceProvider.GetService<IAccountService>();
            var _appSettings = serviceProvider.GetService<IOptionsMonitor<AppSettings>>();
            var user = _accountService.GetCurrentUser(mail);

            if (user != null)
            {
                var _imageStorage = serviceProvider.GetService<IImageStorage>();
                user.ProfilePhoto = _imageStorage.UriFor(user.ProfilePhoto ?? _appSettings.CurrentValue.DefaultProfilePicture);
                user.IsAuthenticated = true;
                return user;
            }
            else
            {
                return new CurrentUser(isAuthenticated: false);
            }
        });

        return services;
    }
}
