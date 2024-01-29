using SocializR.Models.ViewModels;

namespace SocializR.Services;

public class InterestService(ApplicationUnitOfWork unitOfWork, 
    IMapper _mapper) 
    : BaseService<Interest, InterestService>(unitOfWork), IInterestService
{
    public async Task<List<InterestViewModel>> GetAllAsync()
        => await UnitOfWork.Interests.Query
            .OrderBy(i => i.Name)
            .ProjectTo<InterestViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<List<SelectListItem>> GetSelectListAsync()
        => await UnitOfWork.Interests.Query
            .OrderBy(i => i.Name)
            .Select(i => new SelectListItem(i.Name, i.Id.ToString()))
            .ToListAsync();

    public async Task<List<SelectListItem>> GetSelectedSelectListAsync(List<InterestViewModel> userInterests)
    {
        List<SelectListItem> selectedInterests = await GetSelectListAsync();

        if (userInterests.Any())
        {
            foreach (var interest in userInterests)
            {
                var selectedInterest = selectedInterests.FirstOrDefault(i => i.Value == interest.ToString());
                if(selectedInterest != null)
                {
                    selectedInterest.Selected = true;
                }
            }
        }
        else
        {
            selectedInterests.First().Selected = true;
        }

        return selectedInterests;
    }

    public async Task<EditInterestViewModel> GetViewModelAsync(Guid id)
    {
        var model = await UnitOfWork.Interests.Query
            .Where(i => i.Id == id)
            .ProjectTo<EditInterestViewModel>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        model.Interests = await GetSelectListAsync();

        return model;
    }

    public async Task<List<Guid>> GetByUserAsync(Guid id)
        => await UnitOfWork.UserInterests.Query
            .Where(i => i.UserId == id)
            .Select(i => i.Interest.Id)
            .ToListAsync();

    //public List<string> GetByUserId(string id)
    //{
    //    var interests = UnitOfWork.UserInterests.Query
    //        .Where(i => i.UserId.ToString() == id)
    //        .Select(i => i.Interest.Name)
    //        .ToList();

    //    return interests;
    //}

    public async Task EditAsync(EditInterestViewModel model)
    {
        var interest = await GetAsync(model.Id);
        _mapper.Map(model, interest);

        if (model.ParentId == null)
        {
            interest.ParentId = Guid.Empty;
        }

        Update(interest);
    }

    public void Add(EditInterestViewModel model)
    {
        var interest = new Interest();
        _mapper.Map(model, interest);

        Add(interest);
    }

    public async Task DeleteAsync(Guid id)
    {
        var interest = await GetAsync(id);

        if (interest == null)
        {
            return;
        }

        var children = await UnitOfWork.Interests.Query
            .Where(i => i.ParentId == id)
            .ToListAsync();

        children.ForEach(c => c.ParentId = interest.ParentId);
        UpdateRange(children);

        Remove(interest);
    }

    public async Task<List<SelectItem>> GetSelectItemsAsync()
        => await UnitOfWork.Interests.Query
            .OrderBy(i => i.Name)
            .ProjectTo<SelectItem>(_mapper.ConfigurationProvider)
            .ToListAsync();
}
