using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Data.SqlServer.Configs
{
    [ExcludeFromCodeCoverage]
    public class SqlServerPolicyConfiguration
    {
        public int RetryCount { get; set; } = 3;
        public int WaitBetweenRetriesInMilliseonds { get; set; } = 200;
    }
}
