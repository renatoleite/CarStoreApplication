namespace Infrastructure.Data.SqlServer.Context.RetryPolicy
{
    public interface IDatabaseRetryPolicy
    {
        void Execute(Action operation);
    }
}
