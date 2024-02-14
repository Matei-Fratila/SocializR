using ProfileViewModel = SocializR.Models.ViewModels.Profile.ProfileViewModel;

namespace SocializR.Services.Interfaces;
public interface IProfileService
{
    Task<ProfileViewModel> GetProfile(Guid id);
    Task UpdateProfile(EditProfile model);
    RelationTypes GetRelationToCurrentUser(Guid? currentUserId, Guid id);
}
