using Application.Shared.Models;
using Application.UseCases.InsertCar.Commands;
using Domain.Interfaces.Entity;
using Domain.Interfaces.Repositories;
using Domain.ValueObjects;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.InsertCar
{
    public class InsertCarUseCase : IInsertCarUseCase
    {
        private readonly ICarRepository _repository;
        private readonly ILogger<InsertCarUseCase> _logger;
        private readonly IValidator<InsertCarCommand> _validator;
        private readonly IEntityFactory _entityFactory;

        public InsertCarUseCase(
            ILogger<InsertCarUseCase> logger,
            ICarRepository repository,
            IValidator<InsertCarCommand> validator,
            IEntityFactory entityFactory)
        {
            _logger = logger;
            _repository = repository;
            _validator = validator;
            _entityFactory = entityFactory;
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

                _logger.LogInformation("{UseCase} - Inserting car; Model: {model}, Brand: {brand}",
                    nameof(InsertCarUseCase), command.Model, command.Brand);

                var user = new User(command.UserId, command.UserName);
                var car = _entityFactory.NewCar(command.Brand, command.Model, command.Year, user, user);

                var id = await _repository.InsertCarAsync(car, cancellationToken);

                _logger.LogInformation("{UseCase} - Inserted car successfully; Model: {model}, Brand: {brand}",
                    nameof(InsertCarUseCase), command.Model, command.Brand);

                output.AddResult($"Car inserted; Cod: {id}; CorrelationId: {car.CorrelationId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - An unexpected error has occurred; Model: {model}, Brand: {brand}",
                    nameof(InsertCarUseCase), command.Model, command.Brand);

                output.AddErrorMessage($"An unexpected error occurred while inserting the car.");
            }

            return output;
        }
    }
}
