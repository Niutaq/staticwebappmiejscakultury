using Application.CQRS.Announcement.Dtos;
using Application.CQRS.Announcement.Enum;
using Domain.Entities;

namespace Application.Persistance.Interfaces.AnnouncementInterfaces;

public interface IAnnouncementRepository
{
    Task AddAnnouncementAsync(Announcement announcement, CancellationToken cancellationToken);
    Task DeleteAnnouncementAsync(Guid userId, Guid announcementId, CancellationToken cancellationToken);
    Task<List<GetAnnouncementDto>> GetAnnouncementsAsync(State state, CancellationToken cancellationToken);
}