using System.Data;

namespace Infrastructure.Data.SqlServer.Context
{
    public interface IDbConnectionWrapper
    {
        void Open();
        Task<T> QuerySingleAsync<T>(string query, object @params, IDbTransaction? transaction = null);
    }
}
