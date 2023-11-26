using AutoMapper;
using SocializR.Entities.DTOs.Media;
using SocializR.Entities;

namespace SocializR.Web.Code.Mappers
{
    public class MediaMapper : Profile
    {
        public MediaMapper()
        {
            CreateMap<Media, MediaModel>();
        }
    }
}
