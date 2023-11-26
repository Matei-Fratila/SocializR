﻿namespace SocializR.Services;

public class CityService : BaseService
{
    private readonly CurrentUser currentUser;
    private readonly IMapper mapper;

    public CityService(CurrentUser currentUser, SocializRUnitOfWork unitOfWork, IMapper mapper)
        : base(unitOfWork)
    {
        this.mapper = mapper;
        this.currentUser = currentUser;
    }

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

    public List<CityVM> GetCitiesByCountyId(string countyId)
    {
        return UnitOfWork.Cities.Query
            .Where(u => u.CountyId.ToString() == countyId)
            .OrderBy(u => u.Name)
            .ProjectTo<CityVM>(mapper.ConfigurationProvider)
            .ToList();
    }

    public List<SelectListItem> GetCities(string id)
    {
        //if (id == null)
        //{
        //    return null;
        //}

        var cities = GetAll(id);

        return cities.Select(c => new SelectListItem
        {
            Text = c.Name,
            Value = c.Id.ToString()
        })
        .ToList();
    }

    public List<City> GetAll(string countyId)
    {
        if (countyId == null)
        {
            return UnitOfWork.Cities.Query.ToList();
        }

        return UnitOfWork.Cities.Query
            .Where(c => c.CountyId.ToString() == countyId)
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
