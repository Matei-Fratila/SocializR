namespace SocializR.Web.Code.ExtensionMethods;

public static class ConfigurationExtensionMethods
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, IHostEnvironment env)
    {
        services.AddScoped<CountyService>();
        services.AddScoped<CityService>();
        services.AddScoped<AccountService>();
        services.AddScoped<ProfileService>();
        services.AddScoped<InterestService>();
        services.AddScoped<AlbumService>();
        services.AddScoped<FriendRequestService>();
        services.AddScoped<FriendshipService>();
        services.AddScoped<PostService>();
        services.AddScoped<CommentService>();
        services.AddScoped<LikeService>();
        services.AddScoped<FeedService>();
        services.AddScoped<AdminService>();
        services.AddScoped<SearchService>();
        services.AddScoped<MediaService>();
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
            var userService = serviceProvider.GetService<AccountService>();
            var user = userService.Get(mail);

            if (user != null)
            {
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
