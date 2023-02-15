using CityInfo.API.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DataAccess.DbContexts.CityInfoDbContext;

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
            base.OnConfiguring(optionsBuilder);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>().HasData(
            new City("New York City")
            {
                Id = 1,
                Description = "The one with that big park."
            },
            new City("Warsaw")
            {
                Id = 2,
                Description = "The one with a mermaid."
            },
            new City("Paris")
            {
                Id = 3,
                Description = "The one with that big tower."
            }
        );
            
        modelBuilder.Entity<PointOfInterest>().HasData(
            new PointOfInterest("Central Park")
            {
                Id = 1,
                Description = "The most visited urban park in the USA.",
                CityId = 1
            },
            new PointOfInterest("Empire State Building")
            {
                Id = 2,
                Description = "A 102-story skyscraper.",
                CityId = 1
            },
            new PointOfInterest("Eifel Tower")
            {
                Id = 3,
                Description = "",
                CityId = 3
            }
        );
    }
}