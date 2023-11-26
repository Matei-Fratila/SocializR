using SocializR.Entities.DTOs.Common;

namespace SocializR.Entities.DTOs.Friend;

public class FriendManagementVM
{
    public List<FriendrequestVM> FriendRequests { get; set; }
    public List<UserVM> Friends { get; set; }
}
