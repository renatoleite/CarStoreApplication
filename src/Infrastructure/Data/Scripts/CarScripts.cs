using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Data.Scripts
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
            (COD_CORRELATION, DSC_MODEL, DSC_BRAND, NUM_YEAR)
            OUTPUT INSERTED.COD_CAR
            VALUES (@CorrelationId, @Model, @Brand, @Year)";

        private const string SearchCar = @"
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
	            NUM_YEAR = ISNULL(@Year, NUM_YEAR)
            WHERE
	            COD_CAR = @Id";
    }
}
