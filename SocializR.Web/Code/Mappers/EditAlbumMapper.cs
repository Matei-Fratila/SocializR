using AutoMapper;
using SocializR.Entities.DTOs.Album;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocializR.Web.Code.Mappers
{
    public class EditAlbumMapper : Profile
    {
        public EditAlbumMapper()
        {
            CreateMap<AlbumVM, CreateAlbumVM>();
        }
    }
}
