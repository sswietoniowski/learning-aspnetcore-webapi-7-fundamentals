using CityInfo.API.DataAccess.DbContexts.CityInfoDbContext;
using CityInfo.API.DataAccess.Entities;
using CityInfo.API.DataAccess.Repositories.Interfaces;

namespace CityInfo.API.DataAccess.Repositories;

public class CityRepository : Repository<City>, ICityRepository
{
    public CityRepository(CityInfoDbContext context) : base(context)
    {
    }
}