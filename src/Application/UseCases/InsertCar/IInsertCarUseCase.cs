using Application.Shared.Models;
using Application.UseCases.InsertCar.Commands;

namespace Application.UseCases.InsertCar
{
    public interface IInsertCarUseCase
    {
        Task<Output> ExecuteAsync(InsertCarCommand command, CancellationToken cancellationToken);
    }
}
