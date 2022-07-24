using CityInfo.API.DataAccess.DbContexts.CityInfoDbContext;
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
                .AsNoTracking()
                .OrderBy(city => city.Name)
                .ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            var query = _context.Cities.AsNoTracking();

            if (includePointsOfInterest)
            {
                query = query.Include(city => city.PointsOfInterest);
            }

            return await query.FirstOrDefaultAsync(city => city.Id == cityId);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            return await _context.PointsOfInterest
                .Where(point => point.CityId == cityId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            return await _context.PointsOfInterest
                .AsNoTracking()
                .FirstOrDefaultAsync(point => point.CityId == cityId && point.Id == pointOfInterestId);
        }
    }
}
