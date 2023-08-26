using Application.UseCases.InsertCar.Commands;
using Application.UseCases.SearchCar.Commands;
using WebApi.Models;

namespace WebApi.Mappers
{
    public static class InputMappers
    {
        public static InsertCarCommand MapToApplication(this InsertCarInput input) => new InsertCarCommand
        {
            Brand = input.Brand,
            Model = input.Model,
            Year = input.Year,
            CorrelationId = Guid.NewGuid()
        };
    }
}
