using AutoMapper;
using SocializR.Entities;
using SocializR.Entities.DTOs.Map;

namespace SocializR.Web.Code.Mappers
{
    public class CityMapper : Profile
    {
        public CityMapper()
        {
            CreateMap<City, CityVM>();
        }
    }
}
