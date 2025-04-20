using FluentValidation;

namespace Application.CQRS.Comment.Queries.DisplayComments;

public class DisplayCommentsValidator : AbstractValidator<DisplayCommentsQuery>
{
    public DisplayCommentsValidator()
    {
        RuleFor(x => x.PostId)
            .NotEmpty().WithMessage("Należy wybrać post!");
    }
}