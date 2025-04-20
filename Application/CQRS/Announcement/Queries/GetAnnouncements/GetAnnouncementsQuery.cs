using Application.CQRS.Announcement.Dtos;
using Application.CQRS.Announcement.Enum;
using Application.CQRS.Announcement.Responses;
using MediatR;

namespace Application.CQRS.Announcement.Queries.GetAnnouncements;

public record GetAnnouncementsQuery(State State) : IRequest<List<GetAnnouncementDto>>;