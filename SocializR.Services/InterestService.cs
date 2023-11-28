using SocializR.Entities.DTOs.Interest;

namespace SocializR.Services;

public class InterestService : BaseService
{
    private readonly CurrentUser currentUser;
    private readonly IMapper mapper;

    public InterestService(CurrentUser currentUser, SocializRUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        this.mapper = mapper;
        this.currentUser = currentUser;
    }

    public List<InterestVM> GetAllInterests()
    {
        var interests = UnitOfWork.Interests.Query
            .OrderBy(i => i.Name)
            .ProjectTo<InterestVM>(mapper.ConfigurationProvider)
            .ToList();

        return interests;
    }

    public List<SelectListItem> GetAll()
    {
        var interests = UnitOfWork.Interests.Query.ToList();

        interests.Add(new Interest
        {
            Name = "No Interest",
            Id = Guid.Empty
        });

        return new SelectList(interests, nameof(Interest.Id), nameof(Interest.Name))
            .OrderBy(i => i.Value)
            .ToList();
    }

    public List<SelectListItem> GetAllWithSelected(List<Guid> userInterests)
    {
        List<SelectListItem> selectedInterests = GetAll();

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

    public EditInterestVM GetEditModel(string id)
    {
        var model = UnitOfWork.Interests.Query
            .Where(i => i.Id.ToString() == id)
            .ProjectTo<EditInterestVM>(mapper.ConfigurationProvider)
            .FirstOrDefault();

        model.Interests = GetAll();

        return model;
    }

    public List<Guid> GetByUser(Guid id)
    {
        if (id == Guid.Empty)
        {
            id = currentUser.Id;
        }

        var interests = UnitOfWork.UserInterests.Query
            .Where(i => i.UserId == id)
            .Select(i => i.Interest.Id)
            .ToList();

        return interests;
    }

    public List<string> GetByUserId(string id)
    {
        var interests = UnitOfWork.UserInterests.Query
            .Where(i => i.UserId.ToString() == id)
            .Select(i => i.Interest.Name)
            .ToList();

        return interests;
    }

    public bool EditInterest(EditInterestVM model)
    {
        var interest = UnitOfWork.Interests.Query
            .Where(i => i.Id.ToString() == model.Id)
            .FirstOrDefault();

        mapper.Map(model, interest);

        if (model.ParentId == null)
        {
            interest.ParentId = Guid.Empty;
        }

        UnitOfWork.Interests.Update(interest);

        return UnitOfWork.SaveChanges() != 0;
    }

    public bool AddInterest(EditInterestVM model)
    {
        var interest = new Interest
        {
            Name = model.Name,
            ParentId = new Guid(model.ParentId)
        };

        UnitOfWork.Interests.Add(interest);

        return UnitOfWork.SaveChanges() != 0;
    }

    public bool DeleteInterest(string id)
    {
        var interest = UnitOfWork.Interests.Query
            .Where(i => i.Id.ToString() == id)
            .FirstOrDefault();

        var children = UnitOfWork.Interests.Query
            .Where(i => i.ParentId.ToString() == id)
            .ToList();

        children.ForEach(c => c.ParentId = interest.ParentId);

        UnitOfWork.Interests.UpdateRange(children);

        if (interest == null)
        {
            return false;
        }

        UnitOfWork.Interests.Remove(interest);

        return UnitOfWork.SaveChanges() != 0;
    }
}
