using CityInfo.API.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CityInfo.API.DataAccess.DbContexts.CityInfoDbContext
{
    public class CityInfoDbContext : DbContext
    {
        public DbSet<City> Cities => Set<City>();
        public DbSet<PointOfInterest> PointsOfInterest  => Set<PointOfInterest>();

        public CityInfoDbContext(DbContextOptions<CityInfoDbContext> options)
            : base(options)
        {
        }
    }
}
