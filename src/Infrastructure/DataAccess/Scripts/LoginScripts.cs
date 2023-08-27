using Domain.Interfaces.Scripts;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.DataAccess.Scripts
{
    [ExcludeFromCodeCoverage]
    public class LoginScripts : ILoginScripts
    {
        public string InsertUserAsync => InsertUser;
        public string ChangeUserPermissionAsync => ChangeUserPermission;
        public string GetUserByIdAsync => GetUserById;

        private const string InsertUser = @"
            INSERT INTO [dbo].[LOGIN] (NAM_USER, PWD_USER, DSC_ALLOW_ENDPOINTS)
            OUTPUT INSERTED.COD_LOGIN
            VALUES (@Name, @Password, @AllowEndpoints)";

        private const string ChangeUserPermission = @"
            UPDATE [dbo].[LOGIN] SET DSC_ALLOW_ENDPOINTS = @Permission
            WHERE COD_LOGIN = @Id";

        private const string GetUserById = @"
            SELECT
                COD_LOGIN AS Id,
                NAM_USER AS Name,
                PWD_USER AS Password,
                DSC_ALLOW_ENDPOINTS AS Permissions,
                DAT_INC AS CreatedDate
            FROM
                [dbo].[LOGIN]
            WHERE
                COD_LOGIN = @Id";
    }
}
