namespace SocializR.Services;

public class CityService(SocializRUnitOfWork unitOfWork, IMapper _mapper) : BaseService(unitOfWork)
{
    public bool AddCity(string name, string countyId)
    {
        var city = new City
        {
            Name = name,
            CountyId = Guid.Parse(countyId)
        };

        UnitOfWork.Cities.Add(city);
        return UnitOfWork.SaveChanges() != 0;
    }

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

    public List<CityViewModel> GetCitiesByCountyId(string countyId)
    {
        return UnitOfWork.Cities.Query
            .Where(u => u.CountyId.ToString() == countyId)
            .OrderBy(u => u.Name)
            .ProjectTo<CityViewModel>(_mapper.ConfigurationProvider)
            .ToList();
    }

    public List<SelectListItem> GetCities(Guid? id)
    {
        if (id == null)
        {
            return null;
        }

        var cities = GetAll(id);

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

    public bool Delete(string cityId)
    {
        var city = UnitOfWork.Cities.Query
            .Where(c => c.Id.ToString() == cityId)
            .FirstOrDefault();

        if (city == null)
        {
            return false;
        }

        UnitOfWork.Cities.Remove(city);
        return UnitOfWork.SaveChanges() != 0;
    }
}
