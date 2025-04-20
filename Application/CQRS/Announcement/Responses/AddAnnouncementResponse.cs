namespace Application.CQRS.Announcement.Responses;

public record AddAnnouncementResponse(Guid Id, string Message);