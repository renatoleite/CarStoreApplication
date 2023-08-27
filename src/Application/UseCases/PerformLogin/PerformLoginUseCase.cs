using Application.Shared.Models;
using Application.UseCases.PerformLogin.Commands;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Application.UseCases.PerformLogin.Services;
using Application.Shared.Extensions;

namespace Application.UseCases.PerformLogin
{
    public class PerformLoginUseCase : IPerformLoginUseCase
    {
        private readonly ILoginRepository _repository;
        private readonly ILogger<PerformLoginUseCase> _logger;
        private readonly IValidator<UserLoginCommand> _validator;
        private readonly IAuthenticationService _authenticationService;

        public PerformLoginUseCase(
            ILogger<PerformLoginUseCase> logger,
            ILoginRepository repository,
            IValidator<UserLoginCommand> validator,
            IAuthenticationService authenticationService)
        {
            _logger = logger;
            _repository = repository;
            _validator = validator;
            _authenticationService = authenticationService;
        }

        public async Task<Output> ExecuteAsync(UserLoginCommand command, CancellationToken cancellationToken)
        {
            var output = new Output();

            try
            {
                var validationResult = await _validator.ValidateAsync(command);

                output.AddValidationResult(validationResult);

                if (!output.IsValid)
                    return output;

                _logger.LogInformation("{UseCase} - Getting user; Name: {name}",
                    nameof(PerformLoginUseCase), command.Name);

                var result = await _repository.GetUserByNameAsync(command.Name, cancellationToken);

                if (result == null || result.Password != command.Password.CreateSHA256Hash())
                {
                    output.AddErrorMessage("User does not exist");
                    _logger.LogWarning("User does not exist");
                    return output;
                }

                _logger.LogInformation("{UseCase} - Generating authentication token; Name: {name}",
                    nameof(PerformLoginUseCase), command.Name);

                var token = _authenticationService.CreateToken(result.Id, result.Name, result.Roles);
                output.AddResult(token);

                _logger.LogInformation("{UseCase} - Token generated successfully; Name: {name}",
                    nameof(PerformLoginUseCase), command.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - An unexpected error has occurred;",
                    nameof(PerformLoginUseCase));

                output.AddErrorMessage("An unexpected error has occurred");
            }

            return output;
        }
    }
}
