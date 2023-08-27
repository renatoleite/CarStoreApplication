using Domain;
using Domain.Interfaces.Repositories;
using Domain.Interfaces.Scripts;
using Infrastructure.DataAccess.Dtos;
using Infrastructure.DataAccess.Mappers;
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

        public async Task<ILoginUser> GetUserByIdAsync(int id, CancellationToken cancellationToken)
        {
            var @params = new { id };
            var result = await _dbConnectionWrapper.QuerySingleOrDefaultAsync<LoginDto>(_scripts.GetUserByIdAsync, @params, cancellationToken);
            return result.MapToEntity();
        }

        public Task ChangeUserPermissionAsync(int id, string permission, CancellationToken cancellationToken)
        {
            var @params = new { id, permission };
            return _dbConnectionWrapper.ExecuteAsync(_scripts.ChangeUserPermissionAsync, @params, cancellationToken);
        }
    }
}
