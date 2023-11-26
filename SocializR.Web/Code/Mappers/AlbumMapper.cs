using AutoMapper;
using SocializR.Entities.DTOs.Album;
using SocializR.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SocializR.Entities.DTOs.Media;

namespace SocializR.Web.Code.Mappers
{
    public class AlbumMapper : Profile
    {
        public AlbumMapper()
        {
            CreateMap<Media, MediaModel>();

            CreateMap<Album, AlbumVM>()
                .ForMember(a => a.NrOfImages, opt => opt.MapFrom(u => u.Media.Count))
                .ForMember(a => a.CoverId, opt => opt.MapFrom(u => u.Media.OrderByDescending(c=>c.Id).Select(c=>c.Id).FirstOrDefault()));

            CreateMap<Album, EditAlbumVM>();

        }
    }
}
