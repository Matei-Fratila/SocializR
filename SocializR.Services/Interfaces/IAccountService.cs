namespace SocializR.Services.Interfaces;
public interface IAccountService
{
    CurrentUser GetCurrentUser(string email);
    Task<bool> Register(User user);
}
