using MushroomsGuide.API.Repositories;
using StackExchange.Redis;

namespace MushroomsGuide.API.Extensions;

public static class Extensions
{
    public static void AddApplicationServices(this IHostApplicationBuilder builder)
    {
        var multiplexer = ConnectionMultiplexer.Connect("localhost:6379");
        builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);

        builder.Services.AddScoped<IMushroomsRepository, RedisMushroomsRepository>();
    }
}
