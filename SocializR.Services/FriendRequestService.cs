namespace SocializR.Services;

public class FriendRequestService(ApplicationUnitOfWork unitOfWork,
    IMapper _mapper) :
    BaseService<FriendRequest, FriendRequestService>(unitOfWork), IFriendRequestService
{
    public async Task<List<FriendrequestViewModel>> GetAllAsync(Guid requestedUserId)
        => await UnitOfWork.FriendRequests.Query
            .Where(u => u.RequestedUserId == requestedUserId
                && u.RequesterUser.IsDeleted == false)
            .ProjectTo<FriendrequestViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();

    public async Task<int> GetCountAsync(Guid requestedUserId)
        => await UnitOfWork.FriendRequests.Query
            .Include(u => u.RequesterUser)
            .Where(u => u.RequestedUserId == requestedUserId
                && u.RequesterUser.IsDeleted == false)
            .CountAsync();

    public async Task<List<FriendrequestViewModel>> GetPaginatedAsync(Guid requestedUserId, int pageIndex, int pageSize)
        => await UnitOfWork.FriendRequests.Query
            .Where(u => u.RequestedUserId == requestedUserId
                && u.RequesterUser.IsDeleted == false)
            .OrderBy(u => u.RequesterUser.FirstName)
            .ProjectTo<FriendrequestViewModel>(_mapper.ConfigurationProvider)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

    public void Delete(Guid requestedUserId, Guid requesterUserId)
    {
        var friendrequest = UnitOfWork.FriendRequests.Query
            .Where(f => f.RequesterUserId == requestedUserId
                && f.RequestedUserId == requesterUserId
                || f.RequesterUserId == requesterUserId
                && f.RequestedUserId == requestedUserId)
            .FirstOrDefault();

        if (friendrequest == null)
        {
            return;
        }

        Remove(friendrequest);
    }
}
