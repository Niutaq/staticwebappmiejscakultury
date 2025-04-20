namespace Domain.Entities;

public class Comments
{
    public Guid Id { get; set; }
    public DateTimeOffset DateAdded { get; set; }
    public string Message { get; set; }
    
    public Guid UsersId { get; set; }
    public Users Users { get; set; }

    public Guid PlacesId { get; set; }
    public Places Places { get; set; }
}