namespace SocializR.Services;

public class CountyService : BaseService
{
    private readonly SocializRUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CountyService(SocializRUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public bool AddCounty(String name, String shortName)
    {
        var county = new County
        {
            Name = name,
            ShortName = shortName
        };

        unitOfWork.Counties.Add(county);

        return unitOfWork.SaveChanges() != 0;
    }

    public List<CountyVM> GetCounties()
    {
        return unitOfWork.Counties.Query
            .Where(c=>true==true)
            .ProjectTo<CountyVM>(mapper.ConfigurationProvider)
            .OrderBy(c => c.ShortName)
            .ToList();
            
    }

    public bool EditCounty(string id, string name, string shortname)
    {
        var city = unitOfWork.Counties.Query
            .FirstOrDefault(c => c.Id == id);

        if (city == null)
        {
            return false;
        }

        city.Name = name;
        city.ShortName = shortname;
        unitOfWork.Counties.Update(city);

        return unitOfWork.SaveChanges() != 0;
    }

    public int DeleteCounty(string id)
    {
        var county = unitOfWork.Counties.Query
            .Include(c=>c.Cities)
            .Where(c => c.Id == id)
            .FirstOrDefault();

        if (county == null)
        {
            return 1;
        }

        if (county.Cities.Any())
        {
            return 2;
        }

        unitOfWork.Counties.Remove(county);

        return unitOfWork.SaveChanges() == 0 ? 1 : 0;
    }

    public List<SelectListItem> GetSelectCounties()
    {
        var counties = GetAll();

        return counties.Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        })
        .ToList();
    }

    public List<County> GetAll()
    {
        return UnitOfWork.Counties.Query
            .ToList();
    }
}
