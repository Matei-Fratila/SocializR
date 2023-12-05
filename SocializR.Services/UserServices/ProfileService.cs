using Microsoft.AspNetCore.Identity;

namespace SocializR.Services.UserServices;

public class ProfileService(CurrentUser _currentUser, 
    IFriendshipService _friendshipService,
    IFriendRequestService _friendRequestService,
    UserManager<User> _userManager,
    IMapper _mapper) : IProfileService
{
    public byte[] ConvertToByteArray(IFormFile content)
    {
        using (var memoryStream = new MemoryStream())
        {
            content.CopyTo(memoryStream);
            return memoryStream.ToArray();
        }
    }

    public async Task<bool> ChangeProfilePhoto(Guid photoId, Guid userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        user.ProfilePhotoId = photoId;

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public string GetUserPhoto(string id)
    {
        return _userManager.Users
            .Where(u => u.Id.ToString() == id)
            .Select(u => u.ProfilePhoto.FilePath)
            .FirstOrDefault();
    }

    public ProfileViewModel GetEditProfileVM() =>
        _userManager.Users
            .Where(u => u.Id == _currentUser.Id)
            .ProjectTo<ProfileViewModel>(_mapper.ConfigurationProvider)
            .FirstOrDefault();

    public ProfileViewModel GetEditProfileVM(Guid id)
    {
        var user = _userManager.Users
            .Where(u => u.Id == id && u.IsDeleted == false)
            .FirstOrDefault();

        var profile = _userManager.Users
            .Where(u => u.Id == id && u.IsDeleted == false)
            .ProjectTo<ProfileViewModel>(_mapper.ConfigurationProvider)
            .FirstOrDefault();

        return profile;
    }

    public async Task<ViewProfileViewModel> GetViewProfileVM(Guid id)
    {
        var model = await _userManager.Users
           .Include(u => u.ProfilePhoto)
           .Include(u => u.Posts)
           .Where(u => u.Id == id && u.IsDeleted == false)
           .ProjectTo<ViewProfileViewModel>(_mapper.ConfigurationProvider)
           .FirstOrDefaultAsync();

        return model;
    }

    public async Task<bool> UpdateUser(ProfileViewModel model)
    {
        var user = _userManager.Users
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

        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded;
    }

    public async Task<bool> UpdateCurrentUser(ProfileViewModel model)
    {
        var user = _userManager.Users
            .Where(u => u.Id == _currentUser.Id)
            .FirstOrDefault();

        _mapper.Map<ProfileViewModel, User>(model, user);

        var result = await _userManager.UpdateAsync(user);

        return result.Succeeded;
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

        var hasEntries = _friendshipService.Query
            .Where(f => f.FirstUserId.ToString() == id && f.SecondUserId == _currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.Friends;
        }

        hasEntries = _friendshipService.Query
            .Where(f => f.SecondUserId.ToString() == id && f.FirstUserId == _currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.Friends;
        }

        hasEntries = _friendRequestService.Query
            .Where(f => f.RequesterUserId.ToString() == id && f.RequestedUserId == _currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.PendingAccept;
        }

        hasEntries = _friendRequestService.Query
            .Where(f => f.RequestedUserId.ToString() == id && f.RequesterUserId == _currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.RequestedFriendship;
        }

        return RelationTypes.Strangers;
    }
}
