using FluentValidation;

namespace Application.CQRS.Announcement.Commands.DeleteAnnouncement;

public class DeleteAnnouncementValidator : AbstractValidator<DeleteAnnouncementCommand>
{
    public DeleteAnnouncementValidator()
    {
        RuleFor(x => x.AnnouncementId)
            .NotEmpty().WithMessage("Wybierz ogłoszenie!");
    }
}