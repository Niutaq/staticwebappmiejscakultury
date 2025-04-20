namespace Application.Persistance.Interfaces.EmailInterfaces;

public interface IEmailSenderService
{
    Task SendEmailAsync(string email, string subject, string body);
}