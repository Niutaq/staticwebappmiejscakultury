using Application.CQRS.Announcement.Responses;
using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.AnnouncementInterfaces;
using MediatR;

namespace Application.CQRS.Announcement.Commands.AddAnnouncement;

public class AddAnnouncementHandler : IRequestHandler<AddAnnouncementCommand, AddAnnouncementResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IAnnouncementRepository _announcementRepository;

    public AddAnnouncementHandler(ICurrentUserService currentUserService, IAnnouncementRepository announcementRepository)
    {
        _currentUserService = currentUserService;
        _announcementRepository = announcementRepository;
    }
    
    public async Task<AddAnnouncementResponse> Handle(AddAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var announcement = new Domain.Entities.Announcement
        {
            Localization = request.Localization,
            DataDescription = request.DataDescription,
            Description = request.Description,
            Date = request.Date,
            UsersId = userId
        };

        await _announcementRepository.AddAnnouncementAsync(announcement, cancellationToken);

        return new AddAnnouncementResponse(announcement.Id, "Pomyślnie dodano ogłoszenie!");
    }
}