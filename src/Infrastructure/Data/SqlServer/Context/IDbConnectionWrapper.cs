using System.Data;

namespace Infrastructure.Data.SqlServer.Context
{
    public interface IDbConnectionWrapper
    {
        void Open();
        Task<T> QuerySingleAsync<T>(string query, object @params, CancellationToken cancellationToken, IDbTransaction? transaction = null);
        Task<IEnumerable<T>> QueryAsync<T>(string query, object @params, CancellationToken cancellationToken);
        Task ExecuteAsync(string query, object @params, CancellationToken cancellationToken);
    }
}
