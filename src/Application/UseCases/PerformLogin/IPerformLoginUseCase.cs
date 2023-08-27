using Application.Shared.Models;
using Application.UseCases.PerformLogin.Commands;

namespace Application.UseCases.PerformLogin
{
    public interface IPerformLoginUseCase
    {
        Task<Output> ExecuteAsync(LoginUseCommand command, CancellationToken cancellationToken);
    }
}
