using Application.Shared.Models;
using Application.UseCases.UpdateCar.Commands;

namespace Application.UseCases.UpdateCar
{
    public interface IUpdateCarUseCase
    {
        Task<Output> ExecuteAsync(UpdateCarCommand command, CancellationToken cancellationToken);
    }
}
