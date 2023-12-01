namespace SocializR.Services;

public class SearchService(SocializRUnitOfWork unitOfWork, IMapper _mapper) : BaseService(unitOfWork)
{
    public SearchViewModel SearchResults(int pageIndex, int pageSize, string keyword)
    {
        var model = new SearchViewModel
        {
            Results = UnitOfWork.Users.Query
                .Where(u => u.IsDeleted == false && ((u.FirstName + " " + u.LastName).Contains(keyword) ||
                (u.LastName + " " + u.FirstName).Contains(keyword)))
                .ProjectTo<SearchUserViewModel>(_mapper.ConfigurationProvider)
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
