namespace SocializR.Web.Code.Mappers.ValueResolvers;

public class IsLikedByCurrentUserResolver(CurrentUser _currentUser) : IValueResolver<Post, PostVM, bool>
{
    public bool Resolve(Post source, PostVM destination, bool destMember, ResolutionContext context)
    {
        return source.Likes.Any(l => l.UserId == _currentUser.Id);
    }
}
