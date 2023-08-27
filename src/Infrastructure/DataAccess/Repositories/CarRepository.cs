using Domain;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Scripts;
using Infrastructure.DataAccess.Dtos;
using Infrastructure.DataAccess.Mappers;
using Infrastructure.DataAccess.SqlServer.Context;

namespace Infrastructure.DataAccess.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly ICarScripts _scripts;

        public CarRepository(IDbConnectionWrapper dbConnectionWrapper, ICarScripts scripts)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _scripts = scripts;
        }

        public Task<int> InsertCarAsync(ICar car, CancellationToken cancellationToken)
        {
            var @params = new
            {
                CorrelationId = car.CorrelationId,
                Model = car.Model,
                Brand = car.Brand,
                Year = car.Year,
                CodUserInc = car.CreatedBy.Id,
                CodUserUpd = car.UpdatedBy.Id
            };

            return _dbConnectionWrapper.QuerySingleAsync<int>(_scripts.InsertCarAsync, @params, cancellationToken);
        }

        public async Task<IEnumerable<ICar>> SearchCarAsync(string term, CancellationToken cancellationToken)
        {
            var @params = new
            {
                term,
                CorrelationId = Guid.TryParse(term, out var correlationId) ? (Guid?)correlationId : null
            };

            var result = await _dbConnectionWrapper.QueryAsync<CarDto>(_scripts.SearchCarAsync, @params, cancellationToken);
            return result.MapToEntity();
        }

        public async Task<ICar> GetCarByIdAsync(int id, CancellationToken cancellationToken)
        {
            var @params = new { id };
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<CarDto>(_scripts.GetCarByIdAsync, @params, cancellationToken);
            return result.MapToEntity();
        }

        public Task DeleteCarAsync(int id, CancellationToken cancellationToken)
        {
            var @params = new { id };
            return _dbConnectionWrapper.ExecuteAsync(_scripts.DeleteCarAsync, @params, cancellationToken);
        }

        public Task UpdateCarAsync(int id, string? model, string? brand, int? year, int codUser, CancellationToken cancellationToken)
        {
            var @params = new { id, model, brand, year, codUser };
            return _dbConnectionWrapper.ExecuteAsync(_scripts.UpdateCarAsync, @params, cancellationToken);
        }
    }
}
