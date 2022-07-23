using CityInfo.API.DataAccess.DbContexts.CityInfoDbContext;
using CityInfo.API.DataAccess.Entities;
using CityInfo.API.DataAccess.Repositories.Interfaces;

namespace CityInfo.API.DataAccess.Repositories
{
    public class PointOfInterestRepository : Repository<PointOfInterest>, IPointOfInterestRepository
    {
        public PointOfInterestRepository(CityInfoDbContext context) : base(context)
        {
        }
    }
}