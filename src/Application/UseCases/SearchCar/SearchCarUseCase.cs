using Application.Shared.Models;
using FluentValidation;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.Logging;
using Application.UseCases.SearchCar.Commands;
using Application.UseCases.SearchCar.Mappers;

namespace Application.UseCases.SearchCar
{
    public class SearchCarUseCase : ISearchCarUseCase
    {
        private readonly ICarRepository _repository;
        private readonly ILogger<SearchCarUseCase> _logger;
        private readonly IValidator<SearchCarCommand> _validator;

        public SearchCarUseCase(
            ILogger<SearchCarUseCase> logger,
            ICarRepository repository,
            IValidator<SearchCarCommand> validator)
        {
            _logger = logger;
            _repository = repository;
            _validator = validator;
        }

        public async Task<Output> ExecuteAsync(SearchCarCommand command, CancellationToken cancellationToken)
        {
            var output = new Output();

            try
            {
                var validationResult = await _validator.ValidateAsync(command);

                output.AddValidationResult(validationResult);

                if (!output.IsValid)
                    return output;

                _logger.LogInformation("{UseCase} - Search car by term: {term}",
                    nameof(SearchCarUseCase), command.Term);

                var result = await _repository.SearchCarAsync(command.MapToCarDto(), cancellationToken);

                _logger.LogInformation("{UseCase} - Search car finish successfully; term: {term}",
                    nameof(SearchCarUseCase), command.Term);

                output.AddResult(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "{UseCase} - An unexpected error has occurred", nameof(SearchCarUseCase));

                output.AddErrorMessage($"An unexpected error occurred while search the car");
            }

            return output;
        }
    }
}
