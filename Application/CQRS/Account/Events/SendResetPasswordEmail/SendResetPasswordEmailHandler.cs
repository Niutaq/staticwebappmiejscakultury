using Application.CQRS.Account.Responses;
using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.EmailInterfaces;
using MediatR;

namespace Application.CQRS.Account.Events.SendResetPasswordEmail;

public class SendResetPasswordEmailHandler : IRequestHandler<SendResetPasswordEmailEvent, AccountResponse>
{
    private readonly IEmailSenderService _emailSender;
    private readonly IAccountRepository _accountRepository;

    public SendResetPasswordEmailHandler(IEmailSenderService emailSenderService, IAccountRepository accountRepository)
    {
        _emailSender = emailSenderService;
        _accountRepository = accountRepository;
    }
    
    public async Task<AccountResponse> Handle(SendResetPasswordEmailEvent @event, CancellationToken cancellationToken)
    {
        var response = await _accountRepository.GeneratePasswordTokenAsync(@event.Email, cancellationToken);

        var link = SetUrl(response.Token, response.UserId);

        var message = "Kliknij ten link aby zresetować hasło " + Environment.NewLine + link;

        await _emailSender.SendEmailAsync(@event.Email, "Reset hasła", message);

        return new AccountResponse("Wysłano link do resetu hasła na e-maila!");
    }

    private string SetUrl(string token, Guid userId)
    {
        return $"<a href=http://localhost:3000/reset-password?token={token}&userId={userId}>Zresetuj hasło</a>";
    }
}