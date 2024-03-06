using Microsoft.AspNetCore.Identity;

namespace SocializR.Services.UserServices;

public class AdminService(UserManager<User> _userManager, IMapper _mapper) : IAdminService
{
    public async Task<List<UserViewModel>> GetPaginatedUsersAsync(int pageIndex, int pageSize)
        => await _userManager.Users
            .OrderBy(u=>u.FirstName)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<List<UserViewModel>> SearchUsersAsync(string keyWord, int pageIndex, int pageSize)
        => await _userManager.Users
            .Where(u => (u.FirstName + " " + u.LastName).Contains(keyWord) || (u.LastName + " " + u.FirstName).Contains(keyWord))
            .OrderBy(u => u.FirstName)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<int> GetUsersCountAsync()
        => await _userManager.Users.CountAsync();

    public async Task<bool> DeleteUserAsync(Guid id)
    {
        var user = await _userManager.Users
            .Where(u => u.Id == id)
            .FirstOrDefaultAsync();

        if (user == null)
        {
            return false;
        }

        user.IsDeleted = true;
        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded;
    }

    public async Task<bool> ActivateUserAsync(Guid id)
    {
        var user = _userManager.Users
            .Where(u => u.Id == id)
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
