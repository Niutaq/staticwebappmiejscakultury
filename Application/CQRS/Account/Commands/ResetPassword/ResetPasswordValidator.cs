using FluentValidation;

namespace Application.CQRS.Account.Commands.ResetPassword;

public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
{
    public ResetPasswordValidator()
    {
        RuleFor(x => x.ConfirmedPassword).NotEmpty()
            .Equal(x => x.Password)
            .WithMessage("Hasła nie są równe");
    }
}