using AutoMapper;
using SocializR.Entities;
using SocializR.Entities.DTOs.Search;

namespace SocializR.Web.Code.Mappers
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, SearchUserVM>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.FirstName + " " + src.LastName));
        }
    }
}
