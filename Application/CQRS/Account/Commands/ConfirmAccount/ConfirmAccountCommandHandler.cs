using Application.CQRS.Account.Responses;
using Application.Persistance.Interfaces.AccountInterfaces;
using MediatR;

namespace Application.CQRS.Account.Commands.ConfirmAccount;

public sealed class ConfirmAccountCommandHandler : IRequestHandler<ConfirmAccountCommand, AccountResponse>
{
    private readonly IAccountRepository _accountRepository;

    public ConfirmAccountCommandHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AccountResponse> Handle(ConfirmAccountCommand request, CancellationToken cancellationToken)
    {
        await _accountRepository.ConfirmAccountAsync(request.UserId, request.Token, cancellationToken);

        return new AccountResponse("Konto zweryfikowane pomyślnie!");
    }
}