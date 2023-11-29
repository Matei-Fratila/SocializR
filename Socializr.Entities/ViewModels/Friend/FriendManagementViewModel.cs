using SocializR.Models.ViewModels.Common;

namespace SocializR.Models.ViewModels.Friend;

public class FriendManagementViewModel
{
    public List<FriendrequestViewModel> FriendRequests { get; set; }
    public List<UserViewModel> Friends { get; set; }
}
