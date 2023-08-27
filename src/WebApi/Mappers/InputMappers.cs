using Application.UseCases.InsertCar.Commands;
using Application.UseCases.UpdateCar.Commands;
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
            CorrelationId = Guid.NewGuid(),
            UserId = 1,
            UserName = "Renato"
        };

        public static UpdateCarCommand MapToApplication(this UpdateCarInput input, int id) => new UpdateCarCommand
        {
            Brand = input.Brand,
            Model = input.Model,
            Year = input.Year,
            Id = id,
            UserId = 1
        };
    }
}
