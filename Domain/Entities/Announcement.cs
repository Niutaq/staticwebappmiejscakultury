namespace Domain.Entities;

public class Announcement
{
    public Guid Id { get; set; }
    public string Localization { get; set; }
    public string DataDescription { get; set; }
    public DateOnly Date { get; set; }
    public string Description { get; set; }

    public Guid UsersId { get; set; }
    public Users Users { get; set; }
}