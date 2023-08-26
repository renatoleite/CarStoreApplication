using Application.UseCases.SearchCar.Commands;
using Infrastructure.Dtos;

namespace Application.UseCases.SearchCar.Mappers
{
    public static class SearchCarMappers
    {
        public static SearchCarDto MapToCarDto(this SearchCarCommand command) => new SearchCarDto
        {
            Term = command.Term
        };
    }
}
