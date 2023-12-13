namespace SocializR.Services.Mappers;

public class CityMapper : Profile
{
    public CityMapper()
    {
        CreateMap<City, CityViewModel>();
    }
}
