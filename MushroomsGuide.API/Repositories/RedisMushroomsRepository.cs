using MushroomsGuide.API.Model;
using NRedisStack;
using NRedisStack.RedisStackCommands;
using NRedisStack.Search;
using NRedisStack.Search.Literals.Enums;
using StackExchange.Redis;
using System.Text.Json;

namespace MushroomsGuide.API.Repositories;

public class RedisMushroomsRepository : IMushroomsRepository
{
    private readonly ILogger<RedisMushroomsRepository> _logger;
    private readonly IDatabase _database;

    private static RedisKey MushroomKeyPrefix = "mushroom:"u8.ToArray();
    private static RedisKey GetMushroomKey(int mushroomId) => MushroomKeyPrefix.Append(mushroomId.ToString());

    public RedisMushroomsRepository(ILogger<RedisMushroomsRepository> logger, IConnectionMultiplexer redis)
    {
        _logger = logger;
        _database = redis.GetDatabase();
        //SeedDatabase();
    }

    public async Task<Mushroom> GetAsync(int mushroomId)
    {
        JsonCommands jsonCommands = _database.JSON();
        var data = await jsonCommands.GetAsync(GetMushroomKey(mushroomId));

        if (data.IsNull)
        {
            return null;
        }

        return JsonSerializer.Deserialize<Mushroom>((string)data);
    }

    public async Task<bool> UpdateAsync(Mushroom mushroom)
    {
        JsonCommands jsonCommands = _database.JSON();
        return await jsonCommands.SetAsync(GetMushroomKey(mushroom.Id), "$", mushroom);
    }

    public async Task<List<Mushroom>> SearchAsync(string term, int pageIndex, int pageSize)
    {
        if (string.IsNullOrEmpty(term))
        {
            term = "*";
        }

        SearchCommands searchCommands = _database.FT();
        var result = await searchCommands.SearchAsync("idx:mushroom_search", new Query(term).Limit(pageIndex, pageSize));

        List<Mushroom> list = [];
        foreach(var mushroom in result.ToJson())
        {
            list.Add(JsonSerializer.Deserialize<Mushroom>((string)mushroom));
        }

        return list;
    }

    private async void SeedDatabase()
    {
        string json = File.ReadAllText("ciuperci.json");
        MushroomSeed[]? mushrooms = JsonSerializer.Deserialize<MushroomSeed[]>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (mushrooms == null || mushrooms.Length == 0)
        {
            return;
        }

        JsonCommands jsonCommands = _database.JSON();
        foreach (var mushroom in mushrooms)
        {
            await jsonCommands.SetAsync(GetMushroomKey(mushroom.Id), "$", mushroom,
                serializerOptions: new JsonSerializerOptions { IgnoreNullValues = true });
        }

        SearchCommands ft = _database.FT();
        ft.Create("idx:mushroom_search ", new FTCreateParams().On(IndexDataType.JSON).Prefix("mushroom:"),
            new Schema().AddTextField("Denumire", sortable: true, noStem: true)
            .AddTextField("DenumirePopulara", sortable: true)
            .AddTagField("EsteMedicinala")
            .AddNumericField("IdSpeciiAsemanatoare")
            .AddNumericField("MorfologieCorpFructifer")
            .AddNumericField("Comestibilitate")
            .AddNumericField("LocDeFructificatie")
            .AddNumericField("LuniDeAparitie"));
    }
}
