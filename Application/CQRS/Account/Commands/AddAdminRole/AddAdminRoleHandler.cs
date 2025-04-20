using Application.CQRS.Account.Responses;
using Application.Persistance.Interfaces.AccountInterfaces;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.Account.Commands.AddAdminRole;

public class AddAdminRoleHandler : IRequestHandler<AddAdminRoleCommand, AccountResponse>
{
    private readonly IAccountRepository _accountRepository;

    public AddAdminRoleHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }
    
    public async Task<AccountResponse> Handle(AddAdminRoleCommand request, CancellationToken cancellationToken)
    {
        await _accountRepository.AddAdminRoleAsync(request.Email, cancellationToken);

        return new AccountResponse("Nadano role admina!");
    }
}