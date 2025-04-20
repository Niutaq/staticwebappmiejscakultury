using FluentValidation;

namespace Application.CQRS.Posts.Queries.DisplayPosts;

public class DisplayPostsValidator : AbstractValidator<DisplayPostsQuery>
{
    public DisplayPostsValidator()
    {
        RuleFor(x => x.Category)
            .NotEmpty().WithMessage("Wybierz kategorię!");
    }
}