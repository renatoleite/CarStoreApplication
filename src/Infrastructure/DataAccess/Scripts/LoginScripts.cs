using Domain.Interfaces.Scripts;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.DataAccess.Scripts
{
    [ExcludeFromCodeCoverage]
    public class LoginScripts : ILoginScripts
    {
        public string InsertUserAsync => InsertUser;

        private const string InsertUser = @"
            INSERT INTO [dbo].[LOGIN] (NAM_USER, [PWD_USER], [DSC_ALLOW_ENDPOINTS])
            OUTPUT INSERTED.COD_LOGIN
            VALUES (@Name, @Password, @AllowEndpoints)";
    }
}
