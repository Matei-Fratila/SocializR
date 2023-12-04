namespace SocializR.Services;

public class FriendshipService(ApplicationUnitOfWork unitOfWork, 
    IMapper _mapper) 
    : BaseService<Friendship, FriendshipService>(unitOfWork), IFriendshipService
{
    public async Task<int> GetMutualFriendsCountAsync(Guid firstUserId, Guid secondUserId)
        => await UnitOfWork.Friendships.Query
            .Where(u => u.FirstUserId == firstUserId 
                && u.SecondUser.FriendsFirstUser.Where(f => f.SecondUserId == secondUserId).Any())
            .CountAsync();

    public async Task<List<UserViewModel>> GetAllAsync(Guid id)
        => await UnitOfWork.Friendships.Query
            .Where(u => u.FirstUserId == id && u.SecondUser.IsDeleted == false)
            .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<int> GetCountAsync(Guid id)
        => await UnitOfWork.Friendships.Query
            .Include(u => u.SecondUser)
            .Where(u => u.FirstUserId == id
                && u.SecondUser.IsDeleted == false)
            .CountAsync();

    public async Task<List<UserViewModel>> GetPaginatedAsync(Guid id, int pageIndex, int pageSize)
        => await UnitOfWork.Friendships.Query
            .Where(u => u.FirstUserId == id
                && u.SecondUser.IsDeleted == false)
            .ProjectTo<UserViewModel>(_mapper.ConfigurationProvider)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

    public async Task<bool> AreFriendsAsync(Guid firstUserId, Guid secondUserId)
        => await UnitOfWork.Friendships.Query
            .Where(f => (f.FirstUserId == firstUserId && f.SecondUserId == secondUserId ||
                f.FirstUserId == secondUserId && f.SecondUserId == firstUserId) &&
                f.FirstUser.IsDeleted == false && f.SecondUser.IsDeleted == false)
            .AnyAsync();

    public void Create(Guid firstUserId, Guid secondUserId)
    {
        var friendships = new List<Friendship>()
        {
            new() {
                FirstUserId = firstUserId,
                SecondUserId = secondUserId,
                CreatedDate=DateTime.Now

            },
            new() {
                FirstUserId = secondUserId,
                SecondUserId = firstUserId,
                CreatedDate=DateTime.Now
            }
        };

        AddRange(friendships);
    }

    public async Task DeleteAsync(Guid firstUserId, Guid secondUserId)
    {
        var requests = await UnitOfWork.Friendships.Query
            .Where(f => f.FirstUserId == firstUserId && f.SecondUserId == secondUserId ||
            f.SecondUserId == firstUserId && f.FirstUserId == secondUserId)
            .ToListAsync();

        if (requests.Count != 0)
        {
            RemoveRange(requests);
        }
    }
}
