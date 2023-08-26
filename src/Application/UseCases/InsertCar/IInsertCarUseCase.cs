using Application.UseCases.InsertCar.Commands;

namespace Application.UseCases.InsertCar
{
    public interface IInsertCarUseCase
    {
        Task ExecuteAsync(InsertCarCommand command);
    }
}
