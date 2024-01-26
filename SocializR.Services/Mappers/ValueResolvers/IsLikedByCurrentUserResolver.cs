namespace SocializR.Services.Mappers.ValueResolvers;

public class IsLikedByCurrentUserResolver(CurrentUser _currentUser) : IValueResolver<Post, PostViewModel, bool>
{
    public bool Resolve(Post source, PostViewModel destination, bool destMember, ResolutionContext context)
    {
        return source.Likes.Any(l => l.UserId == _currentUser.Id);
    }
}
