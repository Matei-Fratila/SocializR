namespace SocializR.Services.UserServices;

public class AdminService : BaseService
{
    private readonly IMapper mapper;

    public AdminService(SocializRUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        this.mapper = mapper;
    }

    public List<UserVM> GetAllUsers(int pageIndex, int pageSize, out int totalUserCount)
    {
        totalUserCount = UnitOfWork.Users.Query.Count();

        return UnitOfWork.Users.Query
            .OrderBy(u=>u.FirstName)
            .ProjectTo<UserVM>(mapper.ConfigurationProvider)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToList();
    }

    public bool DeleteUser(string id)
    {
        var user = UnitOfWork.Users.Query
            .Where(u => u.Id.ToString() == id)
            .FirstOrDefault();

        if (user == null)
        {
            return false;
        }

        user.IsDeleted = true;
        UnitOfWork.Users.Update(user);
        return UnitOfWork.SaveChanges() != 0;
    }

    public bool ActivateUser(string id)
    {
        var user = UnitOfWork.Users.Query
            .Where(u => u.Id.ToString() == id)
            .FirstOrDefault();

        if (user == null)
        {
            return false;
        }

        user.IsDeleted = false;
        UnitOfWork.Users.Update(user);
        return UnitOfWork.SaveChanges() != 0;
    }
}
