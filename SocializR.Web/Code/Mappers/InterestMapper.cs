namespace SocializR.Web.Code.Mappers;

public class InterestMapper:Profile
{
    public InterestMapper()
    {
        CreateMap<Interest, InterestVM>();

        CreateMap<Interest, EditInterestVM>();

        CreateMap<EditInterestVM, Interest>();
    }
}
