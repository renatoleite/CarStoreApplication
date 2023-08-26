using Application.UseCases.UpdateCar.Commands;
using FluentValidation;

namespace Application.UseCases.UpdateCar.Validation
{
    public class UpdateCarCommandValidator : AbstractValidator<UpdateCarCommand>
    {
        public UpdateCarCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
            RuleFor(x => x.Year).GreaterThan(1500).When(x => x.Year != null);
        }
    }
}
