using CityInfo.API.DataAccess.Entities;
using CityInfo.API.DataAccess.Repositories;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        public Task<IEnumerable<City>> GetCitiesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId)
        {
            throw new NotImplementedException();
        }

        public Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId)
        {
            throw new NotImplementedException();
        }
    }
}
