using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Data.SqlServer.Constants
{
    [ExcludeFromCodeCoverage]
    public static class SqlServerTransientExceptionDetector
    {
        public static bool ShouldRetry(SqlException sqlException)
        {
            foreach (SqlError error in sqlException.Errors)
            {
                switch (error.Number)
                {
                    case 49920:
                    case 49919:
                    case 49918:
                    case 41839:
                    case 41325:
                    case 41305:
                    case 41302:
                    case 41301:
                    case 40613:
                    case 40501:
                    case 40197:
                    case 11001:
                    case 10929:
                    case 10928:
                    case 10060:
                    case 10054:
                    case 10053:
                    case 1205:
                    case 233:
                    case 121:
                    case 64:
                    case 20:
                        return true;
                }
            }

            return false;
        }
    }
}
