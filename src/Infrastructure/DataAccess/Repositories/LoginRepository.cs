//using Infrastructure.DataAccess.Scripts;
//using Infrastructure.DataAccess.SqlServer.Context;
//using Infrastructure.Dtos;

//namespace Infrastructure.DataAccess.Repositories
//{
//    public class LoginRepository : ILoginRepository
//    {
//        private readonly IDbConnectionWrapper _dbConnectionWrapper;
//        private readonly ILoginScripts _scripts;

//        public LoginRepository(IDbConnectionWrapper dbConnectionWrapper, ILoginScripts scripts)
//        {
//            _dbConnectionWrapper = dbConnectionWrapper;
//            _scripts = scripts;
//        }

//        public Task<int> InsertUserAsync(CarDto car, CancellationToken cancellationToken)
//        {
//            return _dbConnectionWrapper.QuerySingleAsync<int>(_scripts.InsertUserAsync, car, cancellationToken);
//        }
//    }
//}
