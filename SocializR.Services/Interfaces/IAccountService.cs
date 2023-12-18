namespace SocializR.Services.Interfaces;
public interface IAccountService
{
    Task<CurrentUser> GetCurrentUser(string email);
    Task<bool> Register(User user);
}
