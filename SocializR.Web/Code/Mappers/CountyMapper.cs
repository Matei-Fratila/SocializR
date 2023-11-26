namespace SocializR.Web.Code.Mappers;

public class CountyMapper : Profile
{
    public CountyMapper()
    {
        CreateMap<County, CountyVM>();
    }
}
