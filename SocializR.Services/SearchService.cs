namespace SocializR.Services;

public class SearchService : BaseService
{
    private readonly CurrentUser currentUser;
    private readonly IMapper mapper;

    public SearchService(CurrentUser currentUser, SocializRUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        this.mapper = mapper;
        this.currentUser = currentUser;
    }

    public SearchViewModel SearchResults(int pageIndex, int pageSize, string keyword)
    {
        var model = new SearchViewModel
        {
            Results = UnitOfWork.Users.Query
                        .Where(u => u.IsDeleted == false && ((u.FirstName + " " + u.LastName).Contains(keyword) ||
                        (u.LastName + " " + u.FirstName).Contains(keyword)))
                        .ProjectTo<SearchUserViewModel>(mapper.ConfigurationProvider)
                        .Skip(pageSize * (pageIndex - 1))
                        .Take(pageSize)
                        .ToList(),

            Total_Count = UnitOfWork.Users.Query
                            .Where(u => (u.FirstName + " " + u.LastName).Contains(keyword) ||
                            (u.LastName + " " + u.FirstName).Contains(keyword))
                            .Count()
        };

        return model;
    }
}
