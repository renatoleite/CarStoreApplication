using Application.UseCases.InsertCar.Commands;
using FluentValidation;

namespace Application.UseCases.InsertCar.Validation
{
    public class InsertCarCommandValidator : AbstractValidator<InsertCarCommand>
    {
        public InsertCarCommandValidator()
        {
            RuleFor(x => x.Model).NotEmpty();
            RuleFor(x => x.Brand).NotEmpty();
            RuleFor(x => x.Year).GreaterThan(1500);
        }
    }
}
