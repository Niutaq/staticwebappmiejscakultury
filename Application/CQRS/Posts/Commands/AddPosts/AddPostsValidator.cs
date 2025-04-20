using FluentValidation;

namespace Application.CQRS.Posts.Commands.AddPosts;

public class AddPostsValidator : AbstractValidator<AddPostsCommand>
{
    public AddPostsValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Tytuł jest wymagany!");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Opis jest wymagany!");

        RuleFor(x => x.Category)
            .IsInEnum().NotEmpty().WithMessage("Należy wybrać kategorię!");

        RuleFor(x => x.LocalizationX)
            .NotNull().WithMessage("Podaj lokalizację!");
        
        RuleFor(x => x.LocalizationY)
            .NotNull().WithMessage("Podaj lokalizację!");
    }
}