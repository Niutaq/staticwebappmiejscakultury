namespace Application.CQRS.Posts.Dtos;

public class DisplayPostsDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double LocalizationX { get; set; }
    public double LocalizationY { get; set; }
    
    public double AverageRating { get; set; }
    public List<string> Images { get; set; }
    public int LikesCount { get; set; }
}