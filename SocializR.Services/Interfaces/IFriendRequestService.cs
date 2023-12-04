namespace SocializR.Services.Interfaces;
public interface IFriendRequestService : IBaseService<FriendRequest>
{
    Task<List<FriendrequestViewModel>> GetAllAsync(Guid requestedUserId);
    Task<List<FriendrequestViewModel>> GetPaginatedAsync(Guid requestedUserId, int pageIndex, int pageSize);
    Task<int> GetCountAsync(Guid requestedUserId);
    void Delete(Guid requestedUserId, Guid requesterUserId);
}
