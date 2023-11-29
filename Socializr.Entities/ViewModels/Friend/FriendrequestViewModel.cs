namespace SocializR.Models.ViewModels.Friend;

public class FriendrequestViewModel
{
    public string Id { get; set; }
    public DateTime CreatedOn { get; set; }
    public string ProfilePhotoId { get; set; }
    public string RequestMessage { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
