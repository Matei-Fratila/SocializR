namespace Socializr.Models.ViewModels.Paging;
public class PostsPagingDto()
{
    public Guid UserId { get; set; }
    public Guid AuthorizedUserId { get; set; }
    public int PageIndex { get; set; } = 1;
    public int PageSize { get; set; }
    public bool IsProfileView { get; set; }
}
