using Infrastructure.Data.Scripts;
using Infrastructure.Data.SqlServer.Context;
using Infrastructure.Dtos;

namespace Infrastructure.Data.Repositories
{
    public class CarRepository
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly ICarScripts _scripts;

        public CarRepository(IDbConnectionWrapper dbConnectionWrapper, ICarScripts scripts)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _scripts = scripts;
        }

        public Task InsertCarAsync(CarDto car)
        {
            return _dbConnectionWrapper.QuerySingleAsync<int>(_scripts.InsertCarAsync, car);
        }
    }
}
