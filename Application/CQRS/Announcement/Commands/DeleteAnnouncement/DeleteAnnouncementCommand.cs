using Application.CQRS.Announcement.Responses;
using MediatR;

namespace Application.CQRS.Announcement.Commands.DeleteAnnouncement;

public record DeleteAnnouncementCommand(
    Guid AnnouncementId
    ) : IRequest<DeleteAnnouncementResponse>;