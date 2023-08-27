using Application.UseCases.PerformLogin.Commands;
using FluentValidation;

namespace Application.UseCases.PerformLogin.Validation
{
    public class LoginUseCommandValidator : AbstractValidator<LoginUseCommand>
    {
        public LoginUseCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MinimumLength(3);
            RuleFor(x => x.Password).NotEmpty().MinimumLength(3);
        }
    }
}
