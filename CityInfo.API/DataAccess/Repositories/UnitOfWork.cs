using CityInfo.API.DataAccess.DbContexts.CityInfoDbContext;
using CityInfo.API.DataAccess.Repositories.Interfaces;

namespace CityInfo.API.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly CityInfoDbContext _context;

    public ICityRepository CityRepository { get; }
    public IPointOfInterestRepository PointOfInterestRepository { get; }

    public UnitOfWork(CityInfoDbContext context)
    {
        _context = context;
        CityRepository = new CityRepository(_context);
        PointOfInterestRepository = new PointOfInterestRepository(_context);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context?.Dispose();
        }
    }
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}