using SocializR.Models.ViewModels;

namespace SocializR.Services.Mappers;

public class CountyMapper : Profile
{
    public CountyMapper()
    {
        CreateMap<County, CountyViewModel>();

        CreateMap<County, SelectItem>()
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()));
    }
}
