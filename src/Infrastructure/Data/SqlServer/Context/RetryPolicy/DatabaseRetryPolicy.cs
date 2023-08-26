using Infrastructure.Data.SqlServer.Configs;
using Infrastructure.Data.SqlServer.Constants;
using Microsoft.Extensions.Options;
using Polly;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Data.SqlServer.Context.RetryPolicy
{
    [ExcludeFromCodeCoverage]
    internal class DatabaseRetryPolicy
    {
        private readonly Policy _retryPolicy;

        public DatabaseRetryPolicy(IOptions<SqlServerPolicyConfig> configuration)
        {
            _retryPolicy = Policy
                .Handle<SqlException>(SqlServerTransientExceptionDetector.ShouldRetry)
                .Or<TimeoutException>()
                .WaitAndRetry(configuration.Value.RetryCount,
                    attemp => TimeSpan.FromMilliseconds(configuration.Value.WaitBetweenRetriesInMilliseonds)
                );
        }

        public void Execute(Action operarion)
        {
            try
            {
                _retryPolicy.Execute(operarion.Invoke);
            }
            catch (Exception ex)
            {
                throw new Exception("Database connection error", ex);
            }
        }
    }
}
