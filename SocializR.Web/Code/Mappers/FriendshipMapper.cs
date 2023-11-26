﻿using AutoMapper;
using SocializR.Entities;
using SocializR.Entities.DTOs.Common;
using SocializR.Entities.DTOs.Friend;

namespace SocializR.Web.Code.Mappers
{
    public class FriendshipMapper : Profile
    {
        public FriendshipMapper()
        {
            CreateMap<Friendship, UserVM>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedDate))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SecondUser.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.SecondUser.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.SecondUser.LastName))
                .ForMember(dest => dest.ProfilePhotoId, opt => opt.MapFrom(src => src.SecondUser.ProfilePhotoId));

            CreateMap<FriendRequest, FriendrequestVM>()
                .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.RequesterUser.Id))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.RequesterUser.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.RequesterUser.LastName))
                .ForMember(dest => dest.ProfilePhotoId, opt => opt.MapFrom(src => src.RequesterUser.ProfilePhotoId));
        }
    }
}
