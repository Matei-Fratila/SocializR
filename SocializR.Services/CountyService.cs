namespace SocializR.Services;

public class CountyService(SocializRUnitOfWork unitOfWork, IMapper _mapper) : BaseService(unitOfWork)
{
    public bool AddCounty(string name, string shortName)
    {
        var county = new County
        {
            Name = name,
            ShortName = shortName
        };

        UnitOfWork.Counties.Add(county);
        return UnitOfWork.SaveChanges() != 0;
    }

    public List<CountyViewModel> GetCounties()
    {
        return UnitOfWork.Counties.Query
            .Where(c=>true==true)
            .ProjectTo<CountyViewModel>(_mapper.ConfigurationProvider)
            .OrderBy(c => c.ShortName)
            .ToList();
            
    }

    public bool EditCounty(string id, string name, string shortname)
    {
        var city = UnitOfWork.Counties.Query
            .FirstOrDefault(c => c.Id.ToString() == id);

        if (city == null)
        {
            return false;
        }

        city.Name = name;
        city.ShortName = shortname;

        UnitOfWork.Counties.Update(city);
        return UnitOfWork.SaveChanges() != 0;
    }

    public int DeleteCounty(string id)
    {
        var county = UnitOfWork.Counties.Query
            .Include(c=>c.Cities)
            .Where(c => c.Id.ToString() == id)
            .FirstOrDefault();

        if (county == null)
        {
            return 1;
        }

        if (county.Cities.Any())
        {
            return 2;
        }

        UnitOfWork.Counties.Remove(county);
        return UnitOfWork.SaveChanges() == 0 ? 1 : 0;
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
        return UnitOfWork.Counties.Query.ToList();
    }
}
