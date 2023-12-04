namespace SocializR.Services.Interfaces;
public interface IProfileService
{
    byte[] ConvertToByteArray(IFormFile content);
    Task<bool> ChangeProfilePhoto(Guid photoId, Guid userId);
    string GetUserPhoto(string id);
    ProfileViewModel GetEditProfileVM();
    ProfileViewModel GetEditProfileVM(Guid id);
    ViewProfileViewModel GetViewProfileVM(Guid id);
    Task<bool> UpdateUser(ProfileViewModel model);
    Task<bool> UpdateCurrentUser(ProfileViewModel model);
    RelationTypes GetRelationToCurrentUser(string currentUserId, string id);
}
