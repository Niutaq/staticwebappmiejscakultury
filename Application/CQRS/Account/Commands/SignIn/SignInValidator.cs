using FluentValidation;

namespace Application.CQRS.Account.Commands.SignIn;

public class SignInValidator : AbstractValidator<SignInCommand>
{
    public SignInValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress().WithMessage("Podaj adres E-mail");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Podaj hasło");
    }
}