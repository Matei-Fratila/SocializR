using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SocializR.Services.UserServices;

public class AdminService(UserManager<User> _userManager, IMapper _mapper) : IAdminService
{
    public List<UserViewModel> GetAllUsers(int pageIndex, int pageSize, out int totalUserCount)
    {
        totalUserCount = _userManager.Users.Count();

        return _userManager.Users
            .OrderBy(u=>u.FirstName)
            .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToList();
    }

    public async Task<bool> DeleteUser(string id)
    {
        var user = _userManager.Users
            .Where(u => u.Id.ToString() == id)
            .FirstOrDefault();

        if (user == null)
        {
            return false;
        }

        user.IsDeleted = true;
        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded;
    }

    public async Task<bool> ActivateUser(string id)
    {
        var user = _userManager.Users
            .Where(u => u.Id.ToString() == id)
            .FirstOrDefault();

        if (user == null)
        {
            return false;
        }

        user.IsDeleted = false;
        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded;
    }
}
