using Application.UseCases.InsertUser.Commands;
using FluentValidation;

namespace Application.UseCases.InsertUser.Validation
{
    public class InsertUserCommandValidator : AbstractValidator<InsertUserCommand>
    {
        public InsertUserCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(3);
        }
    }
}
