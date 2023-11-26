namespace SocializR.Web.Code.Mappers;

public class AccountMapper : Profile
{
    public AccountMapper()
    {
        CreateMap<RegisterVM, User>()
            .ForMember(dest=>dest.UserName, opt=>opt.MapFrom(src=>src.Email));
    }
}
