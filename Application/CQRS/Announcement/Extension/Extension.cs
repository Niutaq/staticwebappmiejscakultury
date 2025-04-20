using Application.CQRS.Announcement.Dtos;

namespace Application.CQRS.Announcement.Extension;

public static class Extension
{
    public static GetAnnouncementDto GetAnnouncementAsDto(this Domain.Entities.Announcement announcement)
    {
        return new GetAnnouncementDto
        {
            Id = announcement.Id,
            Localization = announcement.Localization,
            DataDescription = announcement.DataDescription,
            Date = announcement.Date,
            Description = announcement.Description
        };
    }
}