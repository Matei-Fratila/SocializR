namespace SocializR.Services.Interfaces;
public interface IAdminService
{
    Task<List<UserViewModel>> GetPaginatedUsersAsync(int pageIndex, int pageSize);
    Task<List<UserViewModel>> SearchUsersAsync(string keyWord, int pageIndex, int pageSize);
    Task<int> GetUsersCountAsync();
    Task<bool> DeleteUserAsync(Guid id);
    Task<bool> ActivateUserAsync(Guid id);
}
