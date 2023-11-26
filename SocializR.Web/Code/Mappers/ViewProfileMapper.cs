using AutoMapper;
using SocializR.Entities;
using SocializR.Entities.Enums;
using System.Linq;
using SocializR.Entities.DTOs.Profile;

namespace SocializR.Web.Code.Mappers
{
    public class ViewProfileMapper : Profile
    {
        public ViewProfileMapper()
        {
            CreateMap<User, ViewProfileVM>()
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
                .ForMember(dest => dest.County, opt => opt.MapFrom(src => src.City.County.Name))
                .ForMember(dest => dest.MyInterests, opt => opt.MapFrom(src => src.UserInterests.Select(u => u.Interest.Id)))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => ((GenderTypes)src.Gender).ToString()))
                .ForMember(p => p.FilePath, opt => opt.MapFrom(u => u.ProfilePhoto.FilePath));
        }
    }
}
