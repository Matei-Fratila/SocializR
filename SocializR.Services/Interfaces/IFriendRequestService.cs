namespace SocializR.Services.Interfaces;
public interface IFriendRequestService : IBaseService<FriendRequest>
{
    List<FriendrequestViewModel> GetAllFriendRequests();
    List<FriendrequestViewModel> GetFriendRequests(int pageIndex, int pageSize, out int totalRequestsCount);
    bool DeleteFriendRequest(Guid id);
    bool SendFriendRequest(Guid id);
}
