using MushroomsGuide.API.Model;
using MushroomsGuide.API.ViewModels;
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
    private readonly string _indexName = "idx:mushroom_search";

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
        return await jsonCommands.SetAsync(GetMushroomKey(mushroom.Id), "$", mushroom, 
            serializerOptions: new JsonSerializerOptions { IgnoreNullValues = true});
    }

    public async Task<List<Mushroom>> SearchAsync(string term)
    {
        if (string.IsNullOrEmpty(term))
        {
            term = "*";
        }

        SearchCommands searchCommands = _database.FT();

        var query = new Query(term)
        {
            SortBy = "DenumirePopulara",
        };

        var result = await searchCommands.SearchAsync(_indexName, query.Limit(0, 1000));

        List<Mushroom> list = [];
        foreach(var mushroom in result.ToJson())
        {
            list.Add(JsonSerializer.Deserialize<Mushroom>((string)mushroom));
        }

        return list;
    }

    public IEnumerable<string> GetGenuri()
    {
        SearchCommands searchCommands = _database.FT();

        var query = new Query("*").ReturnFields(new FieldName("$.Denumire", "nume")).Limit(0, 1000);

        HashSet<string> hash = [];
        foreach (var nume in searchCommands.Search(_indexName, query).Documents.Select(x => x["nume"]))
        {
            hash.Add(nume.ToString().Split(' ')[0]);
        }

        return hash.ToArray().OrderBy(x => x);
    }

    public async Task<MushroomsPaginatedViewModel> FilterSearchAsync(SearchFilters filters)
    {
        SearchCommands searchCommands = _database.FT();

        var query = new Query(filters.Query)
        {
            SortBy = filters.SortBy ?? "Id",
            SortAscending = filters.IsAscendingOrder ?? true,
        };

        query.Limit(filters.PageIndex * filters.PageSize, filters.PageSize);

        var result = await searchCommands.SearchAsync(_indexName, query);

        List<Mushroom> list = [];
        foreach (var mushroom in result.ToJson())
        {
            list.Add(JsonSerializer.Deserialize<Mushroom>((string)mushroom));
        }

        return new MushroomsPaginatedViewModel
        {
            Ciuperci = list,
            TotalCount = result.TotalResults
        };
    }

    public async Task<MushroomsPaginatedViewModel> GetPaginatedAsync(int pageIndex, int pageSize)
    {
        SearchCommands searchCommands = _database.FT();
        var result = await searchCommands.SearchAsync("idx:search", 
            new Query { SortBy = "Id"}.Limit(pageIndex * pageSize, pageSize));

        List<Mushroom> list = [];
        foreach (var mushroom in result.ToJson())
        {
            list.Add(JsonSerializer.Deserialize<Mushroom>((string)mushroom));
        }

        return new MushroomsPaginatedViewModel 
        { 
            Ciuperci = list, 
            TotalCount = result.TotalResults
        };
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
