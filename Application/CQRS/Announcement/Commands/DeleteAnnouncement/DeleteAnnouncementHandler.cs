using Application.CQRS.Announcement.Responses;
using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.AnnouncementInterfaces;
using MediatR;

namespace Application.CQRS.Announcement.Commands.DeleteAnnouncement;

public class DeleteAnnouncementHandler : IRequestHandler<DeleteAnnouncementCommand, DeleteAnnouncementResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IAnnouncementRepository _announcementRepository;

    public DeleteAnnouncementHandler(ICurrentUserService currentUserService, IAnnouncementRepository announcementRepository)
    {
        _currentUserService = currentUserService;
        _announcementRepository = announcementRepository;
    }
    
    public async Task<DeleteAnnouncementResponse> Handle(DeleteAnnouncementCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        await _announcementRepository.DeleteAnnouncementAsync(userId, request.AnnouncementId, cancellationToken);

        return new DeleteAnnouncementResponse("Pomyślnie usunięto ogłoszenie!");
    }
}