using System.Threading.Tasks;

namespace SocializR.Services.Interfaces;
public interface IAdminService
{
    List<UserViewModel> GetAllUsers(int pageIndex, int pageSize, out int totalUserCount);
    Task<bool> DeleteUser(string id);
    Task<bool> ActivateUser(string id);
}
