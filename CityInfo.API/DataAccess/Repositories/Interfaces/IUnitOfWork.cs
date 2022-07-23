namespace CityInfo.API.DataAccess.Repositories.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        ICityRepository CityRepository { get; }
        IPointOfInterestRepository PointOfInterestRepository { get; }
        Task SaveAsync();
    }
}
