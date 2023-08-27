using Application.Shared.Extensions;
using Application.Shared.Models;
using Application.UseCases.InsertUser.Commands;
using Domain.Interfaces.Entity;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.InsertUser
{
    public class InsertUserUseCase : IInsertUserUseCase
    {
        private readonly ILoginRepository _repository;
        private readonly ILogger<InsertUserUseCase> _logger;
        private readonly IValidator<InsertUserCommand> _validator;
        private readonly IEntityFactory _entityFactory;

        public InsertUserUseCase(
            ILogger<InsertUserUseCase> logger,
            ILoginRepository repository,
            IValidator<InsertUserCommand> validator,
            IEntityFactory entityFactory)
        {
            _logger = logger;
            _repository = repository;
            _validator = validator;
            _entityFactory = entityFactory;
        }

        public async Task<Output> ExecuteAsync(InsertUserCommand command, CancellationToken cancellationToken)
        {
            var output = new Output();

            try
            {
                var validationResult = await _validator.ValidateAsync(command);

                output.AddValidationResult(validationResult);

                if (!output.IsValid)
                    return output;

                _logger.LogInformation("{UseCase} - Inserting user; Name: {name}",
                    nameof(InsertUserUseCase), command.Name);

                var user = _entityFactory.NewLoginUser(command.Name, command.Password.CreateSHA256Hash(), command.Roles);

                var id = await _repository.InsertUserAsync(user, cancellationToken);

                _logger.LogInformation("{UseCase} - Inserted user successfully; Name: {name}",
                    nameof(InsertUserUseCase), command.Name);

                output.AddResult($"User inserted; Cod: {id}; Name: {command.Name}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - An unexpected error has occurred;",
                    nameof(InsertUserUseCase));

                output.AddErrorMessage($"An unexpected error occurred while inserting the user");
            }

            return output;
        }
    }
}
