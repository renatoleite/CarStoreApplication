using System.Data;

namespace Infrastructure.DataAccess.SqlServer.Context
{
    public interface IDbConnectionWrapper
    {
        void Open();
        Task<T> QuerySingleAsync<T>(string query, object @params, CancellationToken cancellationToken, IDbTransaction? transaction = null);
        Task<IEnumerable<T>> QueryAsync<T>(string query, object @params, CancellationToken cancellationToken);
        Task<T> QuerySingleOrDefaultAsync<T>(string query, object @params, CancellationToken cancellationToken, IDbTransaction? transaction = null);
        Task ExecuteAsync(string query, object @params, CancellationToken cancellationToken);
    }
}
