using MushroomsGuide.API.Model;

namespace MushroomsGuide.API.Repositories;

public interface IMushroomsRepository
{
    Task<Mushroom> GetAsync(int id);
    Task<bool> UpdateAsync(Mushroom mushroom);
    Task<List<Mushroom>> SearchAsync(string term, int pageIndex, int pageSize);

}
