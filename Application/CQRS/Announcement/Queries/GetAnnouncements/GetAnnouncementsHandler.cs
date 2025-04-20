using Application.CQRS.Announcement.Dtos;
using Application.Persistance.Interfaces.AnnouncementInterfaces;
using MediatR;

namespace Application.CQRS.Announcement.Queries.GetAnnouncements;

public class GetAnnouncementsHandler : IRequestHandler<GetAnnouncementsQuery, List<GetAnnouncementDto>>
{
    private readonly IAnnouncementRepository _announcementRepository;

    public GetAnnouncementsHandler(IAnnouncementRepository announcementRepository)
    {
        _announcementRepository = announcementRepository;
    }
    
    public async Task<List<GetAnnouncementDto>> Handle(GetAnnouncementsQuery request, CancellationToken cancellationToken)
    {
        return await _announcementRepository.GetAnnouncementsAsync(request.State, cancellationToken);
    }
}