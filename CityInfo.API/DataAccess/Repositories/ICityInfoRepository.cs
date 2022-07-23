using CityInfo.API.DataAccess.Entities;
using CityInfo.API.DTOs;

namespace CityInfo.API.DataAccess.Repositories
{
    // Alternative approach to the way how the "repository pattern" can be structured
    public interface ICityInfoRepository
    {
        // Whether we would return IEnumerable or IQueryable (and by that we would give the developer more options to query the data at the expense of leaking the
        // query logic) is a matter of discussion.
        Task<IEnumerable<City>> GetCities();
        Task<City> GetCity(int cityId, bool includePointsOfInterest);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCity(int cityId);
        Task<PointOfInterest> GetPointOfInterestForCity(int cityId, int pointOfInterestId);
    }
}
