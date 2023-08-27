using Application.Shared.Models;
using Application.UseCases.ChangeUserPermission.Commands;

namespace Application.UseCases.ChangeUserPermission
{
    public interface IChangeUserPermissionUseCase
    {
        Task<Output> ExecuteAsync(UpdatePermissionCommand command, CancellationToken cancellationToken);
    }
}
