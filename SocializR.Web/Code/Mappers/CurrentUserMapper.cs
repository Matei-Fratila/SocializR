using AutoMapper;
using SocializR.Entities;
using SocializR.Entities.DTOs.Common;
using System.Linq;

namespace SocializR.Web.Code.Mappers
{
    public class CurrentUserMapper:Profile
    {
        public CurrentUserMapper()
        {
            CreateMap<User, CurrentUser>();
        }
    }
}
