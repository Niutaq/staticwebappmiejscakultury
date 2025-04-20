using Application.CQRS.Announcement.Responses;
using MediatR;

namespace Application.CQRS.Announcement.Commands.AddAnnouncement;

public record AddAnnouncementCommand(
    string Localization,
    string DataDescription,
    DateOnly Date,
    string Description
    ) : IRequest<AddAnnouncementResponse>;