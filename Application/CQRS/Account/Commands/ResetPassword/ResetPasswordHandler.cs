using Application.CQRS.Account.Responses;
using Application.Persistance.Interfaces.AccountInterfaces;
using MediatR;

namespace Application.CQRS.Account.Commands.ResetPassword;

public class ResetPasswordHandler : IRequestHandler<ResetPasswordCommand, AccountResponse>
{
    private readonly IAccountRepository _accountRepository;

    public ResetPasswordHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<AccountResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        await _accountRepository.ResetPasswordAssync(request.Token, request.UserId, request.Password,
            cancellationToken);

        return new AccountResponse("Pomyślnie zmieniono hasło!");
    }
}