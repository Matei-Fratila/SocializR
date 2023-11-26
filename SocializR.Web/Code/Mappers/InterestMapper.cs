using AutoMapper;
using SocializR.Entities;
using SocializR.Entities.DTOs.Interest;

namespace SocializR.Web.Code.Mappers
{
    public class InterestMapper:Profile
    {
        public InterestMapper()
        {
            CreateMap<Interest, InterestVM>();

            CreateMap<Interest, EditInterestVM>();

            CreateMap<EditInterestVM, Interest>();
        }
    }
}
