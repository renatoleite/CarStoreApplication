using Infrastructure.Data.Scripts;
using Infrastructure.Data.SqlServer.Context;
using Infrastructure.Dtos;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Data.Repositories
{
    [ExcludeFromCodeCoverage]
    public class CarRepository : ICarRepository
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly ICarScripts _scripts;

        public CarRepository(IDbConnectionWrapper dbConnectionWrapper, ICarScripts scripts)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _scripts = scripts;
        }

        public Task<int> InsertCarAsync(CarDto car, CancellationToken cancellationToken)
        {
            return _dbConnectionWrapper.QuerySingleAsync<int>(_scripts.InsertCarAsync, car, cancellationToken);
        }

        public Task<IEnumerable<CarDto>> SearchCarAsync(SearchCarDto search, CancellationToken cancellationToken)
        {
            var @params = new
            {
                Term = search.Term,
                CorrelationId = Guid.TryParse(search.Term, out var correlationId) ? (Guid?)correlationId : null
            }; 

            return _dbConnectionWrapper.QueryAsync<CarDto>(_scripts.SearchCarAsync, @params, cancellationToken);
        }

        public Task<CarDto> GetCarByIdAsync(int id, CancellationToken cancellationToken)
        {
            var @params = new { id };
            return _dbConnectionWrapper.QuerySingleOrDefaultAsync<CarDto>(_scripts.GetCarByIdAsync, @params, cancellationToken);
        }

        public Task DeleteCarAsync(int id, CancellationToken cancellationToken)
        {
            var @params = new { id };
            return _dbConnectionWrapper.ExecuteAsync(_scripts.DeleteCarAsync, @params, cancellationToken);
        }

        public Task UpdateCarAsync(UpdateCarDto update, CancellationToken cancellationToken)
        {
            return _dbConnectionWrapper.ExecuteAsync(_scripts.UpdateCarAsync, update, cancellationToken);
        }
    }
}
