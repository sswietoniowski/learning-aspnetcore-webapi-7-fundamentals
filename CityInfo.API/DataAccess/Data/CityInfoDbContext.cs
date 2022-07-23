using CityInfo.API.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DataAccess.Data
{
    public class CityInfoDbContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        public CityInfoDbContext(DbContextOptions<CityInfoDbContext> options)
            : base(options)
        {
        }
    }
}
