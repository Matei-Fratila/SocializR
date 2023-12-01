﻿using Utils;

namespace SocializR.Web.Code.Mappers;

public class PostMapper : Profile
{
    public PostMapper()
    {
        CreateMap<Media, MediaViewModel>();

        CreateMap<Like, LikeViewModel>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.UserPhoto, opt => opt.MapFrom(src => src.User.ProfilePhoto.FilePath));

        CreateMap<Comment, CommentViewModel>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.UserPhoto, opt => opt.MapFrom(src => src.User.ProfilePhoto.FilePath))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.TimeAgo()));

        CreateMap<Post, PostVM>()
            .ForMember(dest => dest.NumberOfLikes, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.UserPhoto, opt => opt.MapFrom(src => src.User.ProfilePhoto.FilePath))
            .ForMember(dest => dest.NumberOfComments, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.TimeAgo()));
        //.ForMember(dest => dest.Media.SelectMany(m => m.FilePath), opt => opt.MapFrom(src => src.Media.SelectMany(m => m.FilePath)));
        //.ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.OrderBy(c=>c.CreatedOn)));
    }
}
