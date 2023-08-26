using Application.UseCases.InsertCar.Commands;
using Infrastructure.Dtos;

namespace Application.UseCases.InsertCar.Mappers
{
    public static class InsertCarMappers
    {
        public static CarDto MapToCarDto(this InsertCarCommand command) => new CarDto
        {
            Brand = command.Brand,
            Model = command.Model,
            Year = command.Year,
            CorrelationId = command.CorrelationId,
            CodUserInc = command.CodUser,
            CodUserUpd = command.CodUser
    };
    }
}
