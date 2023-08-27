namespace Infrastructure.DataAccess.SqlServer.Context.RetryPolicy
{
    public interface IDatabaseRetryPolicy
    {
        void Execute(Action operation);
    }
}
