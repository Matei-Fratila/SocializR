using SocializR.Models.ViewModels.Account;

namespace SocializR.Services.Mappers;

public class AccountMapper : Profile
{
    public AccountMapper()
    {
        CreateMap<RegisterViewModel, User>()
            .ForMember(dest=>dest.UserName, opt=>opt.MapFrom(src=>src.Email));
    }
}
