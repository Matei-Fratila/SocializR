using SocializR.Models.Entities;

namespace SocializR.Models.ViewModels.Common;

public class CurrentUser
{
    public CurrentUser()
    {

    }
    public CurrentUser(bool isAuthenticated)
    {
        IsAuthenticated = isAuthenticated;
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsAuthenticated { get; set; }
    public List<Role> Roles { get; set; }

}
