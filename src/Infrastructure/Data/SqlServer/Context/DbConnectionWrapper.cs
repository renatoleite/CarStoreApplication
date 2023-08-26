using Dapper;
using Infrastructure.Data.SqlServer.Context.RetryPolicy;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Data.SqlServer.Context
{
    [ExcludeFromCodeCoverage]
    public class DbConnectionWrapper : IDbConnectionWrapper
    {
        private readonly IDbConnection _dbConnection;
        private readonly IDatabaseRetryPolicy _databaseRetryPolicy;

        public DbConnectionWrapper(
            IDbConnection dbConnection,
            IDatabaseRetryPolicy databaseRetryPolicy)
        {
            _dbConnection = dbConnection;
            _databaseRetryPolicy = databaseRetryPolicy;
        }

        public void Open()
        {
            _databaseRetryPolicy.Execute(() =>
            {
                if (_dbConnection.State != ConnectionState.Open)
                {
                    _dbConnection.Open();
                }
            });
        }

        public Task<T> QuerySingleAsync<T>(string query, object @params, CancellationToken cancellationToken, IDbTransaction? transaction = null) =>
            _dbConnection.QuerySingleAsync<T>(new CommandDefinition(query, @params, transaction, cancellationToken: cancellationToken));

        public Task<IEnumerable<T>> QueryAsync<T>(string query, object @params, CancellationToken cancellationToken) =>
            _dbConnection.QueryAsync<T>(new CommandDefinition(query, @params, cancellationToken: cancellationToken));

        public Task<T> QuerySingleOrDefaultAsync<T>(string query, object @params, CancellationToken cancellationToken, IDbTransaction? transaction = null) =>
            _dbConnection.QuerySingleOrDefaultAsync<T>(new CommandDefinition(query, @params, transaction, cancellationToken: cancellationToken));

        public Task ExecuteAsync(string query, object @params, CancellationToken cancellationToken) =>
            _dbConnection.ExecuteAsync(new CommandDefinition(query, @params, cancellationToken: cancellationToken));
    }
}