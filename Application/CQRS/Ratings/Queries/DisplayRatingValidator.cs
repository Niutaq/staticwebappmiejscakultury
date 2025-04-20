using FluentValidation;

namespace Application.CQRS.Ratings.Queries;

public class DisplayRatingValidator:AbstractValidator<DisplayRatingQuery>
{
    public DisplayRatingValidator()
    {
        RuleFor(x => x.PlaceId).NotEmpty().WithMessage("Należy wybrać post");
    }
}