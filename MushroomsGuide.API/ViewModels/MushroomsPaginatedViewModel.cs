using MushroomsGuide.API.Model;

namespace MushroomsGuide.API.ViewModels;

public class MushroomsPaginatedViewModel
{
    public long TotalCount { get; set; }
    public List<Mushroom> Ciuperci { get; set; } = [];
}
