using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ProfileViewModel = SocializR.Models.ViewModels.Profile.ProfileViewModel;

namespace SocializR.Services.UserServices;

public class ProfileService(CurrentUser _currentUser,
    ApplicationUnitOfWork _unitOfWork,
    UserManager<User> _userManager,
    IOptionsMonitor<AppSettings> _appSettings,
    IFriendshipService _friendshipService,
    IFriendRequestService _friendRequestService,
    IMediaService _mediaService,
    IAlbumService _albumService,
    IImageStorage _imageStorage,
    IPostService _postService,
    IMapper _mapper) : IProfileService
{
    private readonly string _defaultProfilePicture = _appSettings.CurrentValue.DefaultProfilePicture;
    private readonly string _defaultAlbumCover = _appSettings.CurrentValue.DefaultAlbumCover;
    private readonly string _profilePicturesAlbumName = _appSettings.CurrentValue.ProfilePicturesAlbumName;
    private readonly string _fileUploadLocation = _appSettings.CurrentValue.FileUploadLocation;
    private readonly int _postsPerPage = _appSettings.CurrentValue.PostsPerPage;
    private readonly int _commentsPerFirstPage = _appSettings.CurrentValue.CommentsPerFirstPage;

    public async Task<ProfileViewModel> GetProfile(Guid id)
    {
        var model = await _userManager.Users
           .Include(u => u.ProfilePhoto)
           .Where(u => u.Id == id && u.IsDeleted == false)
           .ProjectTo<ProfileViewModel>(_mapper.ConfigurationProvider)
           .FirstOrDefaultAsync();

        return model;
    }

    public async Task UpdateProfile(EditProfile model)
    {
        var user = _userManager.Users
            .Include(u => u.City)
            .Include(u => u.UserInterests)
                .ThenInclude(i => i.Interest)
            .Where(u => u.Id == model.Id)
            .FirstOrDefault();

        _mapper.Map<EditProfile, User>(model, user);
        var result = await _userManager.UpdateAsync(user);

        var file = model.Avatar;

        if (file != null && result.Succeeded)
        {
            var type = file.ContentType.ToString().Split('/');

            if (file.Length > 0)
            {
                if (type[0] == "image")
                {
                    var album = await _albumService.GetAsync(_profilePicturesAlbumName, model.Id);
                    if (album == null)
                    {
                        album = new Album { UserId = model.Id, Name = _profilePicturesAlbumName };
                        _albumService.Add(album);
                    }

                    var imageName = await _imageStorage.SaveImage(file.OpenReadStream(), type[1]);
                    var image = _mediaService.Add(imageName, MediaTypes.Image, album);

                    if (await ChangeProfilePhoto(image.Id, model.Id))
                    {
                        _postService.Add(new Post
                        {
                            UserId = model.Id,
                            Title = "Added a new profile photo",
                            Body = "",
                            CreatedOn = DateTime.Now,
                            Media = [image]
                        });
                    }

                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }
    }

    public RelationTypes GetRelationToCurrentUser(Guid? currentUserId, Guid id)
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
            .Where(f => f.FirstUserId == id
                && f.SecondUserId == _currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.Friends;
        }

        hasEntries = _friendshipService.Query
            .Where(f => f.SecondUserId == id
                && f.FirstUserId == _currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.Friends;
        }

        hasEntries = _friendRequestService.Query
            .Where(f => f.RequesterUserId == id
                && f.RequestedUserId == _currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.PendingAccept;
        }

        hasEntries = _friendRequestService.Query
            .Where(f => f.RequestedUserId == id
                && f.RequesterUserId == _currentUser.Id)
            .Any();

        if (hasEntries)
        {
            return RelationTypes.RequestedFriendship;
        }

        return RelationTypes.Strangers;
    }

    private async Task<bool> ChangeProfilePhoto(Guid photoId, Guid userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
        user.ProfilePhotoId = photoId;

        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }
}
