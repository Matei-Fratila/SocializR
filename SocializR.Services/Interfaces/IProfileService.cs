namespace SocializR.Services.Interfaces;
public interface IProfileService
{
    byte[] ConvertToByteArray(IFormFile content);
    Task<bool> ChangeProfilePhoto(Guid photoId, Guid userId);
    string GetUserPhoto(string id);
    ProfileViewModel GetEditProfileVM();
    ProfileViewModel GetEditProfileVM(Guid id);
    Task<ViewProfileViewModel> GetViewProfileVM(Guid id);
    Task<bool> UpdateUser(ProfileViewModel model);
    Task<bool> UpdateCurrentUser(ProfileViewModel model);
    RelationTypes GetRelationToCurrentUser(Guid? currentUserId, Guid id);
}
