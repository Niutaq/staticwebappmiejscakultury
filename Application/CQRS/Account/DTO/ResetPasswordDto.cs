namespace Application.CQRS.Account.DTO;

public class ResetPasswordDto
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
}