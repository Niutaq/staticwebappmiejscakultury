using FluentValidation;

namespace Application.CQRS.Account.Commands.AddAdminRole;

public class AddAdminRoleValidator : AbstractValidator<AddAdminRoleCommand>
{
    public AddAdminRoleValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Podaj email!");
    }
}