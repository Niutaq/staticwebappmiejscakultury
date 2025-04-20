using FluentValidation;

namespace Application.CQRS.Posts.Commands.UpdatePosts;

public class UpdatePostValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostValidator()
    {
        RuleFor(x => x.PostId)
            .NotNull().WithMessage("Wybierz post do edycji");
    }
}