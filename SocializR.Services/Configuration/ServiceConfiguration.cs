using Microsoft.Extensions.DependencyInjection;
using SocializR.Services.UserServices;

namespace SocializR.Services.Configuration;
public static class ServiceConfiguration
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IAlbumService, AlbumService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<ICountyService, CountyService>();
        services.AddScoped<IFriendRequestService, FriendRequestService>();
        services.AddScoped<IFriendshipService, FriendshipService>();
        services.AddScoped<IInterestService, InterestService>();
        services.AddScoped<ILikeService, LikeService>();
        services.AddScoped<IMediaService, MediaService>();
        services.AddScoped<IPostService, PostService>();

        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IProfileService, ProfileService>();

        return services;
    }
}
