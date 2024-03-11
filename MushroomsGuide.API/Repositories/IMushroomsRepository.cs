using MushroomsGuide.API.Model;
using MushroomsGuide.API.ViewModels;

namespace MushroomsGuide.API.Repositories;

public interface IMushroomsRepository
{
    Task<Mushroom> GetAsync(int id);
    Task<bool> UpdateAsync(Mushroom mushroom);
    Task<List<Mushroom>> SearchAsync(string term);
    Task<MushroomsPaginatedViewModel> FilterSearchAsync(SearchFilters filters);
    Task<MushroomsPaginatedViewModel> GetPaginatedAsync(int pageIndex, int pageSize);
}
