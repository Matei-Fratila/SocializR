namespace SocializR.Web.Code.Mappers;

public class CurrentUserMapper:Profile
{
    public CurrentUserMapper()
    {
        CreateMap<User, CurrentUser>();
    }
}
