using Application.UseCases.ChangeUserPermission.Commands;
using FluentValidation;

namespace Application.UseCases.ChangeUserPermission.Validation
{
    public class UpdatePermissionCommandValidator : AbstractValidator<UpdatePermissionCommand>
    {
        public UpdatePermissionCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}
