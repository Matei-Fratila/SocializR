using AutoMapper;
using SocializR.Entities;
using SocializR.Entities.DTOs.Map;

namespace SocializR.Web.Code.Mappers
{
    public class CountyMapper : Profile
    {
        public CountyMapper()
        {
            CreateMap<County, CountyVM>();
        }
    }
}
