namespace SocializR.Services;

public class CityService(ApplicationUnitOfWork unitOfWork, IMapper _mapper) : BaseService<City, CityService>(unitOfWork), ICityService
{
    #region Read
    public List<CityViewModel> GetAllByCountyId(Guid countyId)
    {
        return UnitOfWork.Cities.Query
            .Where(u => u.CountyId == countyId)
            .OrderBy(u => u.Name)
            .ProjectTo<CityViewModel>(_mapper.ConfigurationProvider)
            .ToList();
    }

    public List<SelectListItem> GetAllByCounty(Guid? countyId)
    {
        if (countyId == null)
        {
            return null;
        }

        var cities = GetAll(countyId).OrderBy(c => c.Name);

        return cities.Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        })
        .ToList();
    }

    public List<City> GetAll(Guid? countyId)
    {
        if (countyId == null)
        {
            return UnitOfWork.Cities.Query.ToList();
        }

        return UnitOfWork.Cities.Query
            .Where(c => c.CountyId == countyId)
            .ToList();
    }
    #endregion

    #region Create
    public bool Create(string name, string countyId)
    {
        var city = new City
        {
            Name = name,
            CountyId = Guid.Parse(countyId)
        };

        UnitOfWork.Cities.Add(city);
        return UnitOfWork.SaveChanges() != 0;
    }
    #endregion

    #region Update
    public bool EditCity(string id, string name)
    {
        var city = UnitOfWork.Cities.Query
            .FirstOrDefault(c => c.Id.ToString() == id);

        if (city == null)
        {
            return false;
        }

        city.Name = name;

        UnitOfWork.Cities.Update(city);
        return UnitOfWork.SaveChanges() != 0;
    }
    #endregion

    #region Delete
    public bool Delete(string id)
    {
        var city = UnitOfWork.Cities.Query
            .Where(c => c.Id.ToString() == id)
            .FirstOrDefault();

        if (city == null)
        {
            return false;
        }

        UnitOfWork.Cities.Remove(city);
        return UnitOfWork.SaveChanges() != 0;
    }
    #endregion
}
