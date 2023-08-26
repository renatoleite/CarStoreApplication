using Application.UseCases.UpdateCar.Commands;
using Infrastructure.Dtos;

namespace Application.UseCases.UpdateCar.Mappers
{
    public static class UpdateCarMappers
    {
        public static UpdateCarDto MapToCarDto(this UpdateCarCommand command) => new UpdateCarDto
        {
            Id = command.Id,
            Brand = command.Brand,
            Model = command.Model,
            Year = command.Year,
            CodUser = command.CodUser,
        };
    }
}
