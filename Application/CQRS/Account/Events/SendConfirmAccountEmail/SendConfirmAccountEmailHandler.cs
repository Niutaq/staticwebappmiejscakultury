using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.EmailInterfaces;
using Domain.Exceptions.MessagesExceptions;
using MediatR;

namespace Application.CQRS.Account.Events.SendConfirmAccountEmail;

public sealed class SendConfirmAccountEmailHandler : INotificationHandler<SendConfirmAccountEmailEvent>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IEmailSenderService _emailSenderService;

    public SendConfirmAccountEmailHandler(IAccountRepository accountRepository, ICurrentUserService currentUserService, IEmailSenderService emailSenderService)
    {
        _accountRepository = accountRepository;
        _currentUserService = currentUserService;
        _emailSenderService = emailSenderService;
    }
    
    public async Task Handle(SendConfirmAccountEmailEvent @event, CancellationToken cancellationToken)
    {
        var userId = @event.UserId ?? _currentUserService.UserId;

        var user = await _accountRepository.GetUserById(userId, cancellationToken)
                   ?? throw new UserNotFoundException();

        var token = await _accountRepository.GenerateEmailConfirmTokenAsync(user, cancellationToken);

        var link = SetUrl(user.Id, token);

        var message = "Click link bellow to activate your account" + Environment.NewLine + link;

        await _emailSenderService.SendEmailAsync(user.Email, "Activate your account", message);
    }

    private string SetUrl(Guid userId, string token)
    {
        return $"\"<a href=http://localhost:3000/confirm-account?token={token}&userId={userId}>Aktywuj konto</a>";
    }
}