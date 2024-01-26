namespace SocializR.Services.Mappers;

public class PostMapper : Profile
{
    public PostMapper()
    {
        CreateMap<Media, MediaViewModel>();

        CreateMap<Like, LikeViewModel>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.UserPhoto, opt => opt.MapFrom(src => src.User.ProfilePhoto.FileName));

        CreateMap<Comment, CommentViewModel>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.UserPhoto, opt => opt.MapFrom(src => src.User.ProfilePhoto.FileName))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.TimeAgo()));

        CreateMap<AddCommentViewModel, Comment>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<Post, PostViewModel>().ConstructUsingServiceLocator()
            .ForMember(dest => dest.NumberOfLikes, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.UserPhoto, opt => opt.MapFrom(src => src.User.ProfilePhoto.FileName))
            .ForMember(dest => dest.NumberOfComments, opt => opt.MapFrom(src => src.Comments.Count))
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn.TimeAgo()));
    }
}

