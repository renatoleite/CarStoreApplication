using Application.UseCases.InsertCar.Commands;

namespace Application.UseCases.InsertCar
{
    public interface IInsertCarUseCase
    {
        Task<string> ExecuteAsync(InsertCarCommand command, CancellationToken cancellationToken);
    }
}
