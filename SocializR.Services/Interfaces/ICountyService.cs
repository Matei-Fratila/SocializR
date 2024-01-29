using SocializR.Models.ViewModels;

namespace SocializR.Services.Interfaces;
public interface ICountyService : IBaseService<County>
{
    Task<List<CountyViewModel>> GetAllAsync();
    Task<List<SelectListItem>> GetSelectListAsync();
    Task<List<SelectItem>> GetSelectItemsAsync();
}
