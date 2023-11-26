namespace SocializR.Web.Code.Mappers;

public class PostMapper : Profile
{
    public PostMapper()
    {
        CreateMap<Media, MediaModel>();

        CreateMap<Like, LikeVM>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.UserPhoto, opt => opt.MapFrom(src => src.User.ProfilePhotoId));

        CreateMap<Comment, CommentVM>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.UserPhoto, opt => opt.MapFrom(src => src.User.ProfilePhotoId));

        CreateMap<Post, PostVM>()
            .ForMember(dest => dest.NumberOfLikes, opt => opt.MapFrom(src => src.Likes.Count))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.User.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.User.LastName))
            .ForMember(dest => dest.UserPhoto, opt => opt.MapFrom(src => src.User.ProfilePhotoId))
            .ForMember(dest => dest.NumberOfComments, opt => opt.MapFrom(src => src.Comments.Count));
            //.ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments.OrderBy(c=>c.CreatedOn)));
    }
}
