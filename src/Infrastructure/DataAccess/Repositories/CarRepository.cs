using Domain;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Scripts;
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
            return _dbConnectionWrapper.QuerySingleAsync<int>(_scripts.InsertCarAsync, car, cancellationToken);
        }

        public Task<IEnumerable<ICar>> SearchCarAsync(string term, CancellationToken cancellationToken)
        {
            var @params = new
            {
                term,
                CorrelationId = Guid.TryParse(term, out var correlationId) ? (Guid?)correlationId : null
            };

            return _dbConnectionWrapper.QueryAsync<ICar>(_scripts.SearchCarAsync, @params, cancellationToken);
        }

        public Task<ICar> GetCarByIdAsync(int id, CancellationToken cancellationToken)
        {
            var @params = new { id };
            return _dbConnectionWrapper.QuerySingleOrDefaultAsync<ICar>(_scripts.GetCarByIdAsync, @params, cancellationToken);
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
