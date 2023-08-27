using Domain.Interfaces.Scripts;
using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.DataAccess.Scripts
{
    [ExcludeFromCodeCoverage]
    public class CarScripts : ICarScripts
    {
        public string InsertCarAsync => InsertCar;
        public string SearchCarAsync => SearchCar;
        public string DeleteCarAsync => DeleteCar;
        public string GetCarByIdAsync => GetCarById;
        public string UpdateCarAsync => UpdateCar;

        private const string InsertCar = @"
            INSERT INTO [dbo].[CAR]
            (COD_CORRELATION, DSC_MODEL, DSC_BRAND, NUM_YEAR, COD_USER_INC, COD_USER_UPD)
            OUTPUT INSERTED.COD_CAR
            VALUES (@CorrelationId, @Model, @Brand, @Year, @CodUserInc, @CodUserUpd)";

        private const string SearchCar = @"
            SELECT
                CAR.COD_CAR AS [Id],
                CAR.COD_CORRELATION AS [CorrelationId],
                CAR.DSC_MODEL AS [Model],
                CAR.DSC_BRAND AS [Brand],
                CAR.NUM_YEAR AS [Year],
                CREATED_USER.COD_LOGIN AS [CreatedUserId],
                CREATED_USER.NAM_USER AS [CreatedUserName],
                CAR.DAT_INC AS [CreatedDate],
                UPDATED_USER.COD_LOGIN AS [UpdatedUserId],
                UPDATED_USER.NAM_USER AS [UpdatedUserName],
                CAR.DAT_UPD AS [UpdatedDate]
            FROM
                [dbo].[CAR]
            INNER JOIN LOGIN AS CREATED_USER ON CAR.COD_USER_INC = CREATED_USER.COD_LOGIN
            INNER JOIN LOGIN AS UPDATED_USER ON CAR.COD_USER_UPD = UPDATED_USER.COD_LOGIN
            WHERE
                DSC_MODEL LIKE @Term OR DSC_BRAND LIKE @Term OR COD_CORRELATION = @CorrelationId";

        private const string GetCarById = @"
            SELECT
                COD_CAR AS [Id],
                COD_CORRELATION AS [CorrelationId],
                DSC_MODEL AS [Model],
                DSC_BRAND AS [Brand],
                NUM_YEAR AS [Year],
                DAT_INC AS [IncDate]
            FROM
                [dbo].[CAR]
            WHERE
                COD_CAR = @Id";

        private const string DeleteCar = "DELETE [dbo].[CAR] WHERE COD_CAR = @Id";

        private const string UpdateCar = @"
            UPDATE
	            [dbo].[CAR]
            SET
	            DSC_MODEL = ISNULL(@Model, DSC_MODEL),
	            DSC_BRAND = ISNULL(@Brand, DSC_BRAND),
	            NUM_YEAR = ISNULL(@Year, NUM_YEAR),
                DAT_UPD = GETUTCDATE(),
                COD_USER_UPD = @CodUser                
            WHERE
	            COD_CAR = @Id";
    }
}
