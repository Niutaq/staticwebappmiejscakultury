namespace Application.CQRS.Announcement.Dtos;

public class GetAnnouncementDto
{
    public Guid Id { get; set; }
    public string Localization { get; set; }
    public string DataDescription { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; }
}