using Application.Shared.Models;
using Application.UseCases.DeleteCar.Commands;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.DeleteCar
{
    public class DeleteCarUseCase : IDeleteCarUseCase
    {
        private readonly ICarRepository _repository;
        private readonly ILogger<DeleteCarUseCase> _logger;
        private readonly IValidator<DeleteCarCommand> _validator;

        public DeleteCarUseCase(
            ILogger<DeleteCarUseCase> logger,
            ICarRepository repository,
            IValidator<DeleteCarCommand> validator)
        {
            _logger = logger;
            _repository = repository;
            _validator = validator;
        }

        public async Task<Output> ExecuteAsync(DeleteCarCommand command, CancellationToken cancellationToken)
        {
            var output = new Output();

            try
            {
                var validationResult = await _validator.ValidateAsync(command);

                output.AddValidationResult(validationResult);

                if (!output.IsValid)
                    return output;

                _logger.LogInformation("{UseCase} - Search car by id: {id}",
                    nameof(DeleteCarUseCase), command.Id);

                var result = await _repository.GetCarByIdAsync(command.Id, cancellationToken);

                if (result == null)
                {
                    output.AddErrorMessage("Car does not exist");
                    _logger.LogWarning("Car does not exist");
                    return output;
                }

                _logger.LogInformation("{UseCase} - Deleting car by id: {id}",
                    nameof(DeleteCarUseCase), command.Id);

                await _repository.DeleteCarAsync(command.Id, cancellationToken);

                _logger.LogInformation("{UseCase} - Car deleted successfully; Id: {id}",
                    nameof(DeleteCarUseCase), command.Id);

                output.AddResult("Car deleted successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - An unexpected error has occurred", nameof(DeleteCarUseCase));

                output.AddErrorMessage($"An unexpected error occurred while deleting the car");
            }

            return output;
        }
    }
}
