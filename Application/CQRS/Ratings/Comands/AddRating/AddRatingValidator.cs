using FluentValidation;

namespace Application.CQRS.Ratings.Comands.AddRating;

public class AddRatingValidator: AbstractValidator<AddRatingCommand>
{
    public AddRatingValidator()
    {
        RuleFor(x => x.Rating).IsInEnum().WithMessage("Wybierz ocenę miejsca");
    }
}