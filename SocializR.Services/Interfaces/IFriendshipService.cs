namespace SocializR.Services.Interfaces;
public interface IFriendshipService : IBaseService<Friendship>
{
    Task<bool> AreFriendsAsync(Guid firstUserId, Guid secondUserId);
    Task<int> GetMutualFriendsCountAsync(Guid firstUserId, Guid secondUserId);
    Task<List<UserViewModel>> GetAllAsync(Guid id);
    Task<int> GetCountAsync(Guid id);
    Task<List<UserViewModel>> GetPaginatedAsync(Guid id, int pageIndex, int pageSize);
    void Create(Guid firstUserId, Guid secondUserId);
    Task DeleteAsync(Guid firstUserId, Guid secondUserId);
}
