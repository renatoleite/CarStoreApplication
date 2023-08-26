using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Data.Scripts
{
    [ExcludeFromCodeCoverage]
    public class CarScripts : ICarScripts
    {
        public string InsertCarAsync => InsertCar;
        public string SearchCarAsync => SearchCar;
        public string DeleteCarAsync => DeleteCar;

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

        private const string DeleteCar = "DELETE [dbo].[CAR] WHERE COD_CAR = @Id";
    }
}
