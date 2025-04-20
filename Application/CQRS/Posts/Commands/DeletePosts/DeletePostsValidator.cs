using FluentValidation;

namespace Application.CQRS.Posts.Commands.DeletePosts;

public class DeletePostsValidator : AbstractValidator<DeletePostsCommand>
{
    public DeletePostsValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("Wybierz poprawny post!");
    }
}