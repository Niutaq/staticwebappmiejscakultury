namespace Infrastructure.Persistance.Repositories.Email.Configuration;

public class SmtpConfig
{
    public string SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUser { get; set; }
    public string SmtpPassword { get; set; }
    public string From { get; set; }
}