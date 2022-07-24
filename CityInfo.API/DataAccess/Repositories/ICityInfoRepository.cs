using CityInfo.API.DataAccess.Entities;

namespace CityInfo.API.DataAccess.Repositories
{
    // Alternative approach to the way how the "repository pattern" can be structured
    public interface ICityInfoRepository
    {
        // Whether we would return IEnumerable or IQueryable (and by that we would give the developer more options to query the data at the expense of leaking the
        // query logic) is a matter of discussion.
        Task<IEnumerable<City>> GetCitiesAsync();
        Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest);
        Task<bool> CityExistsAsync(int cityId);
        Task CreateCityAsync(City city);
        Task UpdateCityAsync(City city);
        Task DeleteCityAsync(City city);
        Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCityAsync(int cityId);
        Task<PointOfInterest?> GetPointOfInterestForCityAsync(int cityId, int pointOfInterestId);
        Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task UpdatePointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task DeletePointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest);
        Task<bool> SaveChangesAsync();
    }
}
