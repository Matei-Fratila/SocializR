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
        var interests = UnitOfWork.Interests.Query
            .ToList();

        var interestSelect = interests.Select(i => new SelectListItem
        {
            Text = i.Name,
            Value = i.Id.ToString()
        })
        .ToList();

        interestSelect.Add(new SelectListItem
        {
            Text = "No Interest",
            Value = "0"
        });

        return interestSelect.OrderBy(i => i.Value).ToList();
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

    public List<string> GetByUser(string id)
    {
        if (id == null)
        {
            id = currentUser.Id;
        }

        var interests = UnitOfWork.UserInterests.Query
            .Where(i => i.UserId.ToString() == id)
            .Select(i => i.Interest.Id.ToString())
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
