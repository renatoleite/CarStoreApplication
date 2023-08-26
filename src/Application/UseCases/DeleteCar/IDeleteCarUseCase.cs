using Application.Shared.Models;
using Application.UseCases.DeleteCar.Commands;

namespace Application.UseCases.DeleteCar
{
    public interface IDeleteCarUseCase
    {
        Task<Output> ExecuteAsync(DeleteCarCommand command, CancellationToken cancellationToken);
    }
}
