namespace SocializR.Services;

public class CountyService(ApplicationUnitOfWork unitOfWork, IMapper _mapper) 
    : BaseService<County, CountyService>(unitOfWork), ICountyService
{
    #region Create
    public bool Create(string name, string shortName)
    {
        var county = new County
        {
            Name = name,
            ShortName = shortName
        };

        UnitOfWork.Counties.Add(county);
        return UnitOfWork.SaveChanges() != 0;
    }
    #endregion

    #region Read
    public List<CountyViewModel> GetAllCities()
    {
        return UnitOfWork.Counties.Query
            .Where(c=>true==true)
            .ProjectTo<CountyViewModel>(_mapper.ConfigurationProvider)
            .OrderBy(c => c.ShortName)
            .ToList();
            
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
    #endregion

    #region Update
    public bool Update(string id, string name, string shortname)
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
    #endregion

    #region Delete
    public int Delete(string id)
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
    #endregion
}
