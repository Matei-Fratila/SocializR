namespace SocializR.Services.Mappers;

public class CountyMapper : Profile
{
    public CountyMapper()
    {
        CreateMap<County, CountyViewModel>();
    }
}
