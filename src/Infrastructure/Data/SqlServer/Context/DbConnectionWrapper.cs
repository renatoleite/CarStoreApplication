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
    }
}