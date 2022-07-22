using CityInfo.API.Models;

namespace CityInfo.API.DataAccess.Data
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public CitiesDataStore()
        {
            Cities = new()
            {
                new()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most visited urban park in the USA."
                        },
                        new()
                        {
                            Id = 2,
                            Name = "Empire State Building",
                            Description = "A 102-story skyscraper."
                        }
                    }
                },
                new()
                {
                    Id = 2,
                    Name = "Warsaw",
                    Description = "The one with a mermaid.",
                },
                new()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with that big tower.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new()
                        {
                            Id = 3,
                            Name = "Eifel Tower"
                        }
                    }
                }
            };
        }
    }
}
