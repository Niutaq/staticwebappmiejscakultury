namespace Application.CQRS.Comment.Dtos;

public class CommentDto
{
    public Guid Id { get; set; }
    public DateTimeOffset DateAdded { get; set; }
    public string Message { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}