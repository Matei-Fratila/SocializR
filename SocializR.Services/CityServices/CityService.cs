﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using SocializR.DataAccess.UnitOfWork;
using SocializR.Entities.DTOs.Map;
using SocializR.Entities;
using SocializR.Entities.DTOs.Common;
using SocializR.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SocializR.Services.CityServices
{
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

        public bool AddCity(String name, string countyId)
        {
            var city = new City
            {
                Name = name,
                CountyId=countyId
            };

            UnitOfWork.Cities.Add(city);

            return UnitOfWork.SaveChanges() != 0;
        }

        public bool EditCity(string id, String name)
        {
            var city = UnitOfWork.Cities.Query
                .FirstOrDefault(c => c.Id == id);

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
                .Where(u => u.CountyId == countyId)
                .OrderBy(u=>u.Name)
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
                .Where(c => c.CountyId == countyId)
                .ToList();
        }

        public bool Delete(string cityId)
        {
            var city = UnitOfWork.Cities.Query
                .Where(c => c.Id == cityId)
                .FirstOrDefault();

            if (city == null)
            {
                return false;
            }

            UnitOfWork.Cities.Remove(city);

            return UnitOfWork.SaveChanges() != 0;
        }
    }
}
