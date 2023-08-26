using Application.Shared.Models;
using Application.UseCases.InsertCar.Commands;
using Application.UseCases.InsertCar.Mappers;
using FluentValidation;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.InsertCar
{
    public class InsertCarUseCase : IInsertCarUseCase
    {
        private readonly ICarRepository _repository;
        private readonly ILogger<InsertCarUseCase> _logger;
        private readonly IValidator<InsertCarCommand> _validator;

        public InsertCarUseCase(
            ILogger<InsertCarUseCase> logger,
            ICarRepository repository,
            IValidator<InsertCarCommand> validator)
        {
            _logger = logger;
            _repository = repository;
            _validator = validator;
        }

        public async Task<Output> ExecuteAsync(InsertCarCommand command, CancellationToken cancellationToken)
        {
            var output = new Output();

            try
            {
                var validationResult = await _validator.ValidateAsync(command);

                output.AddValidationResult(validationResult);

                if (!output.IsValid)
                    return output;

                _logger.LogInformation("{UseCase} - Inserting car; Model: {model}, Brand: {brand}; CorrelationId: {CorrelationId}",
                    nameof(InsertCarUseCase), command.Model, command.Brand, command.CorrelationId);

                var id = await _repository.InsertCarAsync(command.MapToCarDto());

                _logger.LogInformation("{UseCase} - Inserted car successfully; CorrelationId: {CorrelationId}",
                    nameof(InsertCarUseCase), command.CorrelationId);

                output.AddResult($"Car inserted; Cod: {id}; CorrelationId: {command.CorrelationId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - An unexpected error has occurred; CorrelationId: {CorrelationId}",
                    nameof(InsertCarUseCase), command.CorrelationId);

                output.AddErrorMessage($"An unexpected error occurred while inserting the car. CorrelationId {command.CorrelationId}");
            }

            return output;
        }
    }
}
