﻿using SocializR.Models.ViewModels;

namespace SocializR.Services.Interfaces;
public interface ICityService : IBaseService<City>
{
    Task<List<City>> GetAllAsync(Guid countyId);
    Task<List<CityViewModel>> GetAllViewModelsAsync(Guid countyId);
    Task<List<SelectListItem>> GetSelectListByCountyAsync(Guid countyId);
    Task<List<SelectItem>> GetSelectItemsByCountyAsync(Guid countyId);
}
