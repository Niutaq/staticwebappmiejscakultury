using FluentValidation;

namespace Application.CQRS.Announcement.Commands.AddAnnouncement;

public class AddAnnouncementValidator : AbstractValidator<AddAnnouncementCommand>
{
    public AddAnnouncementValidator()
    {
        RuleFor(x => x.Localization)
            .NotEmpty().WithMessage("Podaj gdzie wydarzenie ma się odbyć");

        RuleFor(x => x.DataDescription)
            .NotEmpty().WithMessage("Opisz godziny w jakich odbędzie się wydarzenie");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Opisz wydarzenie");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Podaj date wydarzenia!");
    }
}