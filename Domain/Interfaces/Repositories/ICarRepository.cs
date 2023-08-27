namespace Domain.Interfaces.Repositories
{
    public interface ICarRepository
    {
        Task<int> InsertCarAsync(ICar car, CancellationToken cancellationToken);
        Task<IEnumerable<ICar>> SearchCarAsync(string term, CancellationToken cancellationToken);
        Task<ICar> GetCarByIdAsync(int id, CancellationToken cancellationToken);
        Task DeleteCarAsync(int id, CancellationToken cancellationToken);
        Task UpdateCarAsync(int id, string? model, string? brand, int? year, int codUser, CancellationToken cancellationToken);
    }
}
