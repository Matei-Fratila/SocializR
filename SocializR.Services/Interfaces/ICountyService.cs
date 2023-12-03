namespace SocializR.Services.Interfaces;
public interface ICountyService : IBaseService<County>
{
    #region Create
    bool Create(string name, string shortName);
    #endregion

    #region Read
    List<CountyViewModel> GetAllCities();

    List<SelectListItem> GetSelectCounties();

    List<County> GetAll();
    #endregion

    #region Update
    bool Update(string id, string name, string shortname);
    #endregion

    #region Delete
    int Delete(string id);
    #endregion
}
