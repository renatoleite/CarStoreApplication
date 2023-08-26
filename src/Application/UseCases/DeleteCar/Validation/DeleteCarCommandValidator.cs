using Application.UseCases.DeleteCar.Commands;
using FluentValidation;

namespace Application.UseCases.DeleteCar.Validation
{
    public class DeleteCarCommandValidator : AbstractValidator<DeleteCarCommand>
    {
        public DeleteCarCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
