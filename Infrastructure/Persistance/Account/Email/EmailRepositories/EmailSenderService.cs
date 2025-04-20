using System.Net.Mail;
using Application.Persistance.Interfaces.EmailInterfaces;
using Infrastructure.Persistance.Repositories.Email.Configuration;
using MimeKit;
using MimeKit.Text;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Infrastructure.Persistance.Repositories.Email.EmailRepositories;

public class EmailSenderService : IEmailSenderService
{
    private readonly SmtpConfig _smtpConfig;

    public EmailSenderService(SmtpConfig smtpConfig)
    {
        _smtpConfig = smtpConfig;
    }
    
    public async Task SendEmailAsync(string email, string subject, string body)
    {
        var emailMessage = CreateEmailMessage(email, subject, body, _smtpConfig);

        using var client = new SmtpClient();
        {
            await client.ConnectAsync(_smtpConfig.SmtpHost, _smtpConfig.SmtpPort, true);
            await client.AuthenticateAsync(_smtpConfig.SmtpUser, _smtpConfig.SmtpPassword);
            var status = await client.SendAsync(emailMessage);
            await client.DisconnectAsync(true);
        }
    }

    private MimeMessage CreateEmailMessage(string email, string subject, string body, SmtpConfig smtpConfig)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(MailboxAddress.Parse(smtpConfig.From));
        emailMessage.To.Add(MailboxAddress.Parse(email));
        emailMessage.Subject = subject;
        emailMessage.Body = new TextPart(TextFormat.Html) { Text = body };

        return emailMessage;
    }
}