namespace SocializR.Services.Interfaces;
public interface ICityService : IBaseService<City>
{
    #region Create
    bool Create(string name, string countyId);
    #endregion

    #region Read
    List<CityViewModel> GetAllByCountyId(Guid countyId);

    List<SelectListItem> GetAllByCounty(Guid? countyId);

    List<City> GetAll(Guid? countyId);
    #endregion

    #region Update
    bool EditCity(string id, string name);
    #endregion

    #region Delete
    bool Delete(string id);
    #endregion
}
