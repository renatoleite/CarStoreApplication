using Application.Shared.Models;
using Application.UseCases.InsertUser.Commands;

namespace Application.UseCases.InsertUser
{
    public interface IInsertUserUseCase
    {
        Task<Output> ExecuteAsync(InsertUserCommand command, CancellationToken cancellationToken);
    }
}
