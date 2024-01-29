using SocializR.Models.ViewModels;

namespace SocializR.Services.Mappers;

public class InterestMapper:Profile
{
    public InterestMapper()
    {
        CreateMap<Interest, InterestViewModel>()
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id));

        CreateMap<Interest, SelectItem>()
            .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id.ToString()));


        CreateMap<Interest, EditInterestViewModel>();

        CreateMap<EditInterestViewModel, Interest>();
    }
}
