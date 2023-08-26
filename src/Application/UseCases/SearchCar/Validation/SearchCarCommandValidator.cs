using Application.UseCases.SearchCar.Commands;
using FluentValidation;

namespace Application.UseCases.SearchCar.Validation
{
    public class SearchCarCommandValidator : AbstractValidator<SearchCarCommand>
    {
        public SearchCarCommandValidator()
        {
            RuleFor(x => x.Term).NotEmpty().MinimumLength(3);
        }
    }
}
