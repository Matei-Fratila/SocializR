namespace SocializR.Services.UserServices;

public class ProfileService : BaseService
{
    private readonly CurrentUser currentUser;
    private readonly IMapper mapper;

    public ProfileService(CurrentUser currentUser, SocializRUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        this.currentUser = currentUser;
        this.mapper = mapper;
    }

    public byte[] ConvertToByteArray(IFormFile content)
    {
        using (var memoryStream = new MemoryStream())
        {
            content.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }

    public bool ChangeProfilePhoto(string id, string userId)
    {
        var user = UnitOfWork.Users.Query
            .Include(u => u.ProfilePhoto)
            .FirstOrDefault(u => u.Id.ToString() == userId);

        user.ProfilePhotoId = new Guid(id);

        UnitOfWork.Users.Update(user);

        return UnitOfWork.SaveChanges() != 0;
    }

    public string GetUserPhoto(string id)
    {
        return UnitOfWork.Users.Query
            .Where(u => u.Id.ToString() == id)
            .Select(u => u.ProfilePhoto.FilePath)
            .FirstOrDefault();
    }

    public ProfileVM GetEditProfileVM() =>
        UnitOfWork.Users.Query
            .Where(u => u.Id.ToString() == currentUser.Id)
            .ProjectTo<ProfileVM>(mapper.ConfigurationProvider)
            .FirstOrDefault();

    public ProfileVM GetEditProfileVM(string id)
    {
        var profile = UnitOfWork.Users.Query
            .Where(u => u.Id.ToString() == id && u.IsDeleted == false)
            .ProjectTo<ProfileVM>(mapper.ConfigurationProvider)
            .FirstOrDefault();

        return profile;
    }

    public ViewProfileVM GetViewProfileVM(string id)
    {
        var result = UnitOfWork.Users.Query
           .Where(u => u.Id.ToString() == id && u.IsDeleted == false)
           .ProjectTo<ViewProfileVM>(mapper.ConfigurationProvider)
           .FirstOrDefault();

        return result;
    }

    public bool UpdateUser(ProfileVM model)
    {
        var user = UnitOfWork.Users.Query
            .Include(u => u.City)
            .Include(u => u.UserInterests)
                .ThenInclude(i => i.Interest)
            .Where(u => u.Id.ToString() == model.Id)
            .FirstOrDefault();

        if (user == null)
        {
            return false;
        }

        mapper.Map<ProfileVM, User>(model, user);

        UnitOfWork.Users.Update(user);

        return UnitOfWork.SaveChanges() != 0;
    }

    public bool UpdateCurrentUser(ProfileVM model)
    {
        var user = UnitOfWork.Users.Query
            .Where(u => u.Id.ToString() == currentUser.Id)
            .FirstOrDefault();

        mapper.Map<ProfileVM, User>(model, user);

        UnitOfWork.Users.Update(user);

        return UnitOfWork.SaveChanges() != 0;
    }

    public RelationTypes GetRelationToCurrentUser(string currentUserId, string id)
    {
        if (currentUserId == null)
        {
            return RelationTypes.Unknown;
        }

        if (currentUserId == id)
        {
            return RelationTypes.Unknown;
        }

        var hasEntries = UnitOfWork.Friendships.Query
            .Where(f => f.FirstUserId.ToString() == id && f.SecondUserId.ToString() == currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.Friends;
        }

        hasEntries = UnitOfWork.Friendships.Query
            .Where(f => f.SecondUserId.ToString() == id && f.FirstUserId.ToString() == currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.Friends;
        }

        hasEntries = UnitOfWork.FriendRequests.Query
            .Where(f => f.RequesterUserId.ToString() == id && f.RequestedUserId.ToString() == currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.PendingAccept;
        }

        hasEntries = UnitOfWork.FriendRequests.Query
            .Where(f => f.RequestedUserId.ToString() == id && f.RequesterUserId.ToString() == currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.RequestedFriendship;
        }

        return RelationTypes.Strangers;
    }
}
