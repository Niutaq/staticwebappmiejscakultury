using FluentValidation;

namespace Application.CQRS.Account.Commands.CreateAccount;

public class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Należy podać imię");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Należy podać nazwisko");

        RuleFor(x => x.Email)
            .NotEmpty().EmailAddress().WithMessage("Należy podać poprawny email");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Hasło musi mieć minimum 8 znaków");

        RuleFor(x => x.RepeatPassword)
            .NotEmpty().Equal(x => x.Password).WithMessage("Hasła nie są identyczne");
    }
}