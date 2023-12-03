namespace SocializR.Services.Interfaces;
public interface IFriendshipService : IBaseService<Friendship>
{
    bool AreFriends(Guid firstUserId, Guid secondUserId);
    int CountMutualFriends(Guid id);
    List<UserViewModel> GetAllFriends();
    List<UserViewModel> GetFriends(int pageIndex, int pageSize, out int totalFriendsCount);
    bool AddFriend(Guid id);
    bool Unfriend(string id);
}
