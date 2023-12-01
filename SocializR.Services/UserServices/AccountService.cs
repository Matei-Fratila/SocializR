namespace SocializR.Services.UserServices;

public class AccountService(SocializRUnitOfWork unitOfWork, 
    IMapper _mapper) : BaseService(unitOfWork)
{
    public CurrentUser Get(string email)
    {
        return UnitOfWork.Users.Query
            .Where(u=>u.Email==email)
            .ProjectTo<CurrentUser>(_mapper.ConfigurationProvider)
            .FirstOrDefault();
    }

    //TODO: add users in PublicUser role
    public bool Register(User user)
    {
        user.IsActive = true;
        user.IsDeleted = false;
        user.CreatedOn = DateTime.Now;

        UnitOfWork.Users.Add(user);

        return UnitOfWork.SaveChanges() != 0;
    }
}
