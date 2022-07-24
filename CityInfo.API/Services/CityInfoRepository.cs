﻿using CityInfo.API.DataAccess.DbContexts.CityInfoDbContext;
using CityInfo.API.DataAccess.Entities;
using CityInfo.API.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoDbContext _context;

        public CityInfoRepository(CityInfoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities
                .OrderBy(city => city.Name)
                .ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            var query = _context.Cities.AsQueryable();

            if (includePointsOfInterest)
            {
                query = query.Include(city => city.PointsOfInterest);
            }

            return await query.FirstOrDefaultAsync(city => city.Id == cityId);
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(city => city.Id == cityId);
        }

        public async Task CreateCityAsync(City city)
        {
            _context.Cities.Add(city);
        }

        public async Task UpdateCityAsync(City city)
        {
            _context.Cities.Update(city);
        }
        
        public async Task DeleteCityAsync(City city)
        {
            _context.Cities.Remove(city);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest
                .Where(point => point.CityId == cityId)
                .ToListAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointsOfInterest
                .FirstOrDefaultAsync(point => point.CityId == cityId && point.Id == pointOfInterestId);
        }

        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city is not null)
            {
                city.PointsOfInterest.Add(pointOfInterest);
            }
        }

        public async Task UpdatePointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Update(pointOfInterest);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Remove(pointOfInterest);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }
    }
}