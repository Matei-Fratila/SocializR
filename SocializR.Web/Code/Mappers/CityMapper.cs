namespace SocializR.Web.Code.Mappers;

public class CityMapper : Profile
{
    public CityMapper()
    {
        CreateMap<City, CityViewModel>();
    }
}
