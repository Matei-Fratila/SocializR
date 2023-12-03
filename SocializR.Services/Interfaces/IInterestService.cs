namespace SocializR.Services.Interfaces;
public interface IInterestService : IBaseService<Interest>
{
    List<InterestViewModel> GetAllInterests();
    List<SelectListItem> GetAll();
    List<SelectListItem> GetAllWithSelected(List<Guid> userInterests);
    EditInterestViewModel GetEditModel(string id);
    List<Guid> GetByUser(Guid id);
    List<string> GetByUserId(string id);
    bool EditInterest(EditInterestViewModel model);
    bool AddInterest(EditInterestViewModel model);
    bool DeleteInterest(string id);
}
