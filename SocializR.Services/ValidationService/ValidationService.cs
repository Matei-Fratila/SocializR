using Microsoft.AspNetCore.Identity;

namespace SocializR.Services.ValidationService;

public class ValidationService(UserManager<User> _userManager, IAlbumService _albumService) : IValidationService 
{
    public bool EmailExists(string email)
    {
        return _userManager.Users.Any(u => u.Email == email);
    }

    public bool AlbumExists(string name, Guid albumId, Guid userId)
    {
        return _albumService.Query.Any(a => a.Name == name && a.UserId == userId && a.Id != albumId);
    }
}
