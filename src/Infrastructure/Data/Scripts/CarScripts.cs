using System.Diagnostics.CodeAnalysis;

namespace Infrastructure.Data.Scripts
{
    [ExcludeFromCodeCoverage]
    public class CarScripts : ICarScripts
    {
        public string InsertCarAsync => InsertCar;
        public string SearchCarAsync => SearchCar;
        public string DeleteCarAsync => DeleteCar;

        private const string InsertCar = "";
        private const string SearchCar = "";
        private const string DeleteCar = "";
    }
}
