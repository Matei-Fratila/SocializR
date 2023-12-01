namespace SocializR.Web.Controllers;

public class SearchController(IMapper _mapper, 
    SearchService _searchService) : BaseController(_mapper)
{
    [HttpGet]
    public JsonResult Search(int page, string search)
    {
        var data = _searchService.SearchResults(page, 5, search);

        return Json(data);
    }
}