using SocializR.Entities.DTOs.Friend;

namespace SocializR.Services;

public class FriendRequestService : BaseService
{
    private readonly CurrentUser currentUser;
    private readonly IMapper mapper;

    public FriendRequestService(CurrentUser currentUser, SocializRUnitOfWork unitOfWork, IMapper mapper) 
        : base(unitOfWork)
    {
        this.mapper = mapper;
        this.currentUser = currentUser;
    }

    public List<FriendrequestVM> GetAllFriendRequests()
    {
        var friends1 = UnitOfWork.FriendRequests.Query
            //.Include(u => u.RequesterUser)
            .Where(u => u.RequestedUserId == currentUser.Id && u.RequesterUser.IsDeleted==false)
                .ProjectTo<FriendrequestVM>(mapper.ConfigurationProvider)
            .ToList();

        return friends1;
    }

    public List<FriendrequestVM> GetFriendRequests(int pageIndex, int pageSize, out int totalRequestsCount)
    {
        totalRequestsCount = UnitOfWork.FriendRequests.Query
            .Include(u => u.RequesterUser)
            .Where(u => u.RequestedUserId == currentUser.Id && u.RequesterUser.IsDeleted == false).Count();

        return UnitOfWork.FriendRequests.Query
            //.Include(u => u.RequesterUser)
            .Where(u => u.RequestedUserId == currentUser.Id && u.RequesterUser.IsDeleted == false)
                .ProjectTo<FriendrequestVM>(mapper.ConfigurationProvider)
                .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToList();
    }

    public bool DeleteFriendRequest(Guid id)
    {
        var friendrequest = UnitOfWork.FriendRequests.Query
            .Where(f => f.RequesterUserId == id 
            && f.RequestedUserId == currentUser.Id 
            || f.RequesterUserId == currentUser.Id 
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
            RequesterUserId = currentUser.Id,
            CreatedOn = DateTime.Now
        };

        UnitOfWork.FriendRequests.Add(friendRequest);

        return UnitOfWork.SaveChanges() != 0;
    }
}
