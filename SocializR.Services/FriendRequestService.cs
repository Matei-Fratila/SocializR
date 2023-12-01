namespace SocializR.Services;

public class FriendRequestService(CurrentUser _currentUser, SocializRUnitOfWork unitOfWork, IMapper _mapper) : BaseService(unitOfWork)
{
    public List<FriendrequestViewModel> GetAllFriendRequests()
    {
        var friends1 = UnitOfWork.FriendRequests.Query
            //.Include(u => u.RequesterUser)
            .Where(u => u.RequestedUserId == _currentUser.Id && u.RequesterUser.IsDeleted==false)
                .ProjectTo<FriendrequestViewModel>(_mapper.ConfigurationProvider)
            .ToList();

        return friends1;
    }

    public List<FriendrequestViewModel> GetFriendRequests(int pageIndex, int pageSize, out int totalRequestsCount)
    {
        totalRequestsCount = UnitOfWork.FriendRequests.Query
            .Include(u => u.RequesterUser)
            .Where(u => u.RequestedUserId == _currentUser.Id && u.RequesterUser.IsDeleted == false).Count();

        return UnitOfWork.FriendRequests.Query
            //.Include(u => u.RequesterUser)
            .Where(u => u.RequestedUserId == _currentUser.Id && u.RequesterUser.IsDeleted == false)
                .ProjectTo<FriendrequestViewModel>(_mapper.ConfigurationProvider)
                .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToList();
    }

    public bool DeleteFriendRequest(Guid id)
    {
        var friendrequest = UnitOfWork.FriendRequests.Query
            .Where(f => f.RequesterUserId == id 
            && f.RequestedUserId == _currentUser.Id 
            || f.RequesterUserId == _currentUser.Id 
            && f.RequestedUserId == id)
            .FirstOrDefault();

        if (friendrequest == null)
        {
            return false;
        }

        UnitOfWork.FriendRequests.Remove(friendrequest);

        return UnitOfWork.SaveChanges() != 0;
    }

    public bool SendFriendRequest(Guid id)
    {
        var friendRequest = new FriendRequest
        {
            RequestedUserId = id,
            RequesterUserId = _currentUser.Id,
            CreatedOn = DateTime.Now
        };

        UnitOfWork.FriendRequests.Add(friendRequest);

        return UnitOfWork.SaveChanges() != 0;
    }
}
