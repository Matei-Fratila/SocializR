using SocializR.Models.ViewModels;

namespace SocializR.Services.Interfaces;
public interface IInterestService : IBaseService<Interest>
{
    Task<List<InterestViewModel>> GetAllAsync();
    Task<List<SelectItem>> GetSelectItemsAsync();
    Task<List<SelectListItem>> GetSelectListAsync();
    Task<List<SelectListItem>> GetSelectedSelectListAsync(List<InterestViewModel> userInterests);
    Task<EditInterestViewModel> GetViewModelAsync(Guid id);
    Task<List<Guid>> GetByUserAsync(Guid id);
    //List<string> GetByUserId(string id);
    Task EditAsync(EditInterestViewModel model);
    void Add(EditInterestViewModel model);
    Task DeleteAsync(Guid id);
}
