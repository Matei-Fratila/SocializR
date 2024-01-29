using SocializR.Models.ViewModels;

namespace SocializR.Services.Mappers;

public class CityMapper : Profile
{
    public CityMapper()
    {
        CreateMap<City, CityViewModel>();

        CreateMap<City, SelectItem>()
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()));
    }
}
