using CityInfo.API.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace CityInfo.API.DataAccess.DbContexts.CityInfoDbContext
{
    public class CityInfoDbContext : DbContext
    {
        public DbSet<City> Cities => Set<City>();
        public DbSet<PointOfInterest> PointsOfInterest => Set<PointOfInterest>();
        // Alternatively: to eliminate null warnings, given that these properties won't be null, we might use:
        // public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!

        public CityInfoDbContext(DbContextOptions<CityInfoDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // We could configure DbContext inside Program class using DI or by overriding some logic inside this method
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source=.\\default.db");
            }
        }
    }
}
