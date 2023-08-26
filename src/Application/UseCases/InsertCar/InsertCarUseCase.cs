using Application.UseCases.InsertCar.Commands;
using Application.UseCases.InsertCar.Mappers;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.Logging;

namespace Application.UseCases.InsertCar
{
    public class InsertCarUseCase : IInsertCarUseCase
    {
        private readonly ICarRepository _repository;
        private readonly ILogger<InsertCarUseCase> _logger;

        public InsertCarUseCase(
            ILogger<InsertCarUseCase> logger,
            ICarRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<string> ExecuteAsync(InsertCarCommand command, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("{UseCase} - Inserting car; Model: {model}, Brand: {brand}; CorrelationId: {CorrelationId}",
                    nameof(InsertCarUseCase), command.Model, command.Brand, command.CorrelationId);

                var id = await _repository.InsertCarAsync(command.MapToCarDto());

                _logger.LogInformation("{UseCase} - Inserted car successfully; CorrelationId: {CorrelationId}",
                    nameof(InsertCarUseCase), command.CorrelationId);

                return $"Car inserted; Cod: {id}; CorrelationId: {command.CorrelationId}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - An unexpected error has occurred; CorrelationId: {CorrelationId}",
                    nameof(InsertCarUseCase), command.CorrelationId);

                return $"An unexpected error occurred while inserting the car. CorrelationId {command.CorrelationId}";
            }
        }
    }
}
