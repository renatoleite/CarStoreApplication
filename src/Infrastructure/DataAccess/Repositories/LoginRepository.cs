using Domain;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Scripts;
using Infrastructure.DataAccess.SqlServer.Context;

namespace Infrastructure.DataAccess.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private readonly IDbConnectionWrapper _dbConnectionWrapper;
        private readonly ILoginScripts _scripts;

        public LoginRepository(IDbConnectionWrapper dbConnectionWrapper, ILoginScripts scripts)
        {
            _dbConnectionWrapper = dbConnectionWrapper;
            _scripts = scripts;
        }

        public Task<int> InsertUserAsync(ILoginUser user, CancellationToken cancellationToken)
        {
            return _dbConnectionWrapper.QuerySingleAsync<int>(_scripts.InsertUserAsync, user, cancellationToken);
        }
    }
}
