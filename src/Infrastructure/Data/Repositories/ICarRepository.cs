using Infrastructure.Dtos;

namespace Infrastructure.Data.Repositories
{
    public interface ICarRepository
    {
        Task InsertCarAsync(CarDto car);
    }
}
