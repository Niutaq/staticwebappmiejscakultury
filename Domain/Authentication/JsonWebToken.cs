namespace Domain.Authentication;

public class JsonWebToken
{
    public string AccessToken { get; init; }
    public long Expires { get; init; }
    public Guid UserId { get; init; }
    public string Email { get; set; }
    public ICollection<string> Roles { get; set; } = new List<string>();
    public IDictionary<string, string> Claims { get; init; } = new Dictionary<string, string>();
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? AvatarUrl { get; set; } = string.Empty;
}