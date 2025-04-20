using FluentValidation;

namespace Application.CQRS.Comment.Commands.UpdateComment;

public class UpdateCommentValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentValidator()
    {
        RuleFor(x => x.CommentId)
            .NotNull().WithMessage("Wybierz komentarz do edycji!");

        RuleFor(x => x.NewMessage)
            .NotEmpty().WithMessage("Nie możesz dodać pustego komentarza");
    }
}