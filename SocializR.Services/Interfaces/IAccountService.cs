using System.Threading.Tasks;

namespace SocializR.Services.Interfaces;
public interface IAccountService
{
    CurrentUser Get(string email);
    Task<bool> Register(User user);
}
