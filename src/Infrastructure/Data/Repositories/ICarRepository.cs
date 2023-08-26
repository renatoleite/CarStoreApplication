using Infrastructure.Dtos;

namespace Infrastructure.Data.Repositories
{
    public interface ICarRepository
    {
        Task<int> InsertCarAsync(CarDto car);
    }
}
