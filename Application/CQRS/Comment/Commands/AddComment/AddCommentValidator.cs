using FluentValidation;

namespace Application.CQRS.Comment.Commands.AddComment;

public class AddCommentValidator : AbstractValidator<AddCommentCommand>
{
    public AddCommentValidator()
    {
        RuleFor(x => x.Message)
            .NotEmpty().WithMessage("Nie można dodać pustego komentarza!");
    }
}