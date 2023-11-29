namespace SocializR.Web.Code.Mappers;

public class InterestMapper:Profile
{
    public InterestMapper()
    {
        CreateMap<Interest, InterestViewModel>();

        CreateMap<Interest, EditInterestViewModel>();

        CreateMap<EditInterestViewModel, Interest>();
    }
}
