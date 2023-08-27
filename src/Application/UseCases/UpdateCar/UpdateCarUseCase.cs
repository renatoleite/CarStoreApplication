using Application.Shared.Models;
using Application.UseCases.UpdateCar.Commands;
using Domain.Interfaces.Repositories;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.UpdateCar
{
    public class UpdateCarUseCase : IUpdateCarUseCase
    {
        private readonly ICarRepository _repository;
        private readonly ILogger<UpdateCarUseCase> _logger;
        private readonly IValidator<UpdateCarCommand> _validator;

        public UpdateCarUseCase(
            ILogger<UpdateCarUseCase> logger,
            ICarRepository repository,
            IValidator<UpdateCarCommand> validator)
        {
            _logger = logger;
            _repository = repository;
            _validator = validator;
        }

        public async Task<Output> ExecuteAsync(UpdateCarCommand command, CancellationToken cancellationToken)
        {
            var output = new Output();

            try
            {
                var validationResult = await _validator.ValidateAsync(command);

                output.AddValidationResult(validationResult);

                if (!output.IsValid)
                    return output;

                _logger.LogInformation("{UseCase} - Search car by id: {id}",
                    nameof(UpdateCarUseCase), command.Id);

                var result = await _repository.GetCarByIdAsync(command.Id, cancellationToken);

                if (result == null)
                {
                    output.AddErrorMessage("Car does not exist");
                    _logger.LogWarning("Car does not exist");
                    return output;
                }

                _logger.LogInformation("{UseCase} - Updating car by id: {id}",
                    nameof(UpdateCarUseCase), command.Id);

                await _repository.UpdateCarAsync(command.Id, command.Model, command.Brand,
                    command.Year, command.UserId, cancellationToken);

                _logger.LogInformation("{UseCase} - Car updated successfully; Id: {id}",
                    nameof(UpdateCarUseCase), command.Id);

                output.AddResult("Car updated successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - An unexpected error has occurred", nameof(UpdateCarUseCase));

                output.AddErrorMessage($"An unexpected error occurred while updating the car");
            }

            return output;
        }
    }
}
