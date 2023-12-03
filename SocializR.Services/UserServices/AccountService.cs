using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SocializR.Services.UserServices;

public class AccountService(UserManager<User> _userManager, IMapper _mapper) : IAccountService
{
    public CurrentUser Get(string email)
    {
        return _userManager.Users
            .Where(u=>u.Email==email)
            .ProjectTo<CurrentUser>(_mapper.ConfigurationProvider)
            .FirstOrDefault();
    }

    //TODO: add users in PublicUser role
    public async Task<bool> Register(User user)
    {
        user.IsActive = true;
        user.IsDeleted = false;
        user.CreatedOn = DateTime.Now;

        var result = await _userManager.CreateAsync(user);

        return result.Succeeded;
    }
}
