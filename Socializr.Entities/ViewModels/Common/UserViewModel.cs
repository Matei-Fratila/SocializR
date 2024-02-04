namespace SocializR.Models.ViewModels.Common;

public class UserViewModel
{
    public string Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ProfilePhoto { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
