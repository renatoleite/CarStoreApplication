using Application.Shared.Models;
using Application.UseCases.SearchCar.Commands;

namespace Application.UseCases.SearchCar
{
    public interface ISearchCarUseCase
    {
        Task<Output> ExecuteAsync(SearchCarCommand command, CancellationToken cancellationToken);
    }
}
