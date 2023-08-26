using Infrastructure.Dtos;

namespace Infrastructure.Data.Repositories
{
    public interface ICarRepository
    {
        Task<int> InsertCarAsync(CarDto car, CancellationToken cancellationToken);
        Task<IEnumerable<CarDto>> SearchCarAsync(SearchCarDto search, CancellationToken cancellationToken);
    }
}
