namespace SocializR.Services.UserServices;

public class ProfileService(CurrentUser _currentUser, 
    SocializRUnitOfWork unitOfWork, 
    IMapper _mapper) : BaseService(unitOfWork)
{
    public byte[] ConvertToByteArray(IFormFile content)
    {
        using (var memoryStream = new MemoryStream())
        {
            content.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }

    public bool ChangeProfilePhoto(Guid photoId, Guid userId)
    {
        var user = UnitOfWork.Users.Query.FirstOrDefault(u => u.Id == userId);
        user.ProfilePhotoId = photoId;
        return UnitOfWork.SaveChanges() != 0;
    }

    public string GetUserPhoto(string id)
    {
        return UnitOfWork.Users.Query
            .Where(u => u.Id.ToString() == id)
            .Select(u => u.ProfilePhoto.FilePath)
            .FirstOrDefault();
    }

    public ProfileViewModel GetEditProfileVM() =>
        UnitOfWork.Users.Query
            .Where(u => u.Id == _currentUser.Id)
            .ProjectTo<ProfileViewModel>(_mapper.ConfigurationProvider)
            .FirstOrDefault();

    public ProfileViewModel GetEditProfileVM(Guid id)
    {
        var user = UnitOfWork.Users.Query
            .Where(u => u.Id == id && u.IsDeleted == false)
            .FirstOrDefault();

        var profile = UnitOfWork.Users.Query
            .Where(u => u.Id == id && u.IsDeleted == false)
            .ProjectTo<ProfileViewModel>(_mapper.ConfigurationProvider)
            .FirstOrDefault();

        return profile;
    }

    public ViewProfileViewModel GetViewProfileVM(Guid id)
    {
        var result = UnitOfWork.Users.Query
           .Include(u => u.ProfilePhoto)
           .Where(u => u.Id == id && u.IsDeleted == false)
           .ProjectTo<ViewProfileViewModel>(_mapper.ConfigurationProvider)
           .FirstOrDefault();

        return result;
    }

    public bool UpdateUser(ProfileViewModel model)
    {
        var user = UnitOfWork.Users.Query
            .Include(u => u.City)
            .Include(u => u.UserInterests)
                .ThenInclude(i => i.Interest)
            .Where(u => u.Id == model.Id)
            .FirstOrDefault();

        if (user == null)
        {
            return false;
        }

        _mapper.Map<ProfileViewModel, User>(model, user);

        UnitOfWork.Users.Update(user);

        return UnitOfWork.SaveChanges() != 0;
    }

    public bool UpdateCurrentUser(ProfileViewModel model)
    {
        var user = UnitOfWork.Users.Query
            .Where(u => u.Id == _currentUser.Id)
            .FirstOrDefault();

        _mapper.Map<ProfileViewModel, User>(model, user);

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
            .Where(f => f.FirstUserId.ToString() == id && f.SecondUserId == _currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.Friends;
        }

        hasEntries = UnitOfWork.Friendships.Query
            .Where(f => f.SecondUserId.ToString() == id && f.FirstUserId == _currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.Friends;
        }

        hasEntries = UnitOfWork.FriendRequests.Query
            .Where(f => f.RequesterUserId.ToString() == id && f.RequestedUserId == _currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.PendingAccept;
        }

        hasEntries = UnitOfWork.FriendRequests.Query
            .Where(f => f.RequestedUserId.ToString() == id && f.RequesterUserId == _currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.RequestedFriendship;
        }

        return RelationTypes.Strangers;
    }
}
