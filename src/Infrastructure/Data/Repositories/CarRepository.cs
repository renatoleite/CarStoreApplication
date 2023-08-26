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

        public Task<int> InsertCarAsync(CarDto car)
        {
            return _dbConnectionWrapper.QuerySingleAsync<int>(_scripts.InsertCarAsync, car);
        }
    }
}
