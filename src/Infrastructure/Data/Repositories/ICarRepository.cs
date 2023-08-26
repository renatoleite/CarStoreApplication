using Infrastructure.Dtos;

namespace Infrastructure.Data.Repositories
{
    public interface ICarRepository
    {
        Task<int> InsertCarAsync(CarDto car, CancellationToken cancellationToken);
        Task<IEnumerable<CarDto>> SearchCarAsync(SearchCarDto search, CancellationToken cancellationToken);
        Task<CarDto> GetCarByIdAsync(int id, CancellationToken cancellationToken);
        Task DeleteCarAsync(int id, CancellationToken cancellationToken);
    }
}
