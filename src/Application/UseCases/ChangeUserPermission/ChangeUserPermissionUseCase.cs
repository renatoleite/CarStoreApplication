using Application.Shared.Models;
using Application.UseCases.ChangeUserPermission.Commands;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.ChangeUserPermission
{
    public class ChangeUserPermissionUseCase : IChangeUserPermissionUseCase
    {
        private readonly ILoginRepository _repository;
        private readonly ILogger<ChangeUserPermissionUseCase> _logger;
        private readonly IValidator<UpdatePermissionCommand> _validator;

        public ChangeUserPermissionUseCase(
            ILogger<ChangeUserPermissionUseCase> logger,
            ILoginRepository repository,
            IValidator<UpdatePermissionCommand> validator)
        {
            _logger = logger;
            _repository = repository;
            _validator = validator;
        }

        public async Task<Output> ExecuteAsync(UpdatePermissionCommand command, CancellationToken cancellationToken)
        {
            var output = new Output();

            try
            {
                var validationResult = await _validator.ValidateAsync(command);

                output.AddValidationResult(validationResult);

                if (!output.IsValid)
                    return output;

                _logger.LogInformation("{UseCase} - Search user by id: {id}",
                    nameof(ChangeUserPermissionUseCase), command.Id);

                var result = await _repository.GetUserByIdAsync(command.Id, cancellationToken);

                if (result == null)
                {
                    output.AddErrorMessage("User does not exist");
                    _logger.LogWarning("User does not exist");
                    return output;
                }

                _logger.LogInformation("{UseCase} - Updating user by id: {id}",
                    nameof(ChangeUserPermissionUseCase), command.Id);

                await _repository.ChangeUserPermissionAsync(command.Id, command.Roles, cancellationToken);

                _logger.LogInformation("{UseCase} - User permission updated successfully; Id: {id}",
                    nameof(ChangeUserPermissionUseCase), command.Id);

                output.AddResult("Car updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - An unexpected error has occurred", nameof(ChangeUserPermissionUseCase));

                output.AddErrorMessage($"An unexpected error occurred while updating the user permission");
            }

            return output;
        }
    }
}
