using Application.CQRS.Account.Responses;
using MediatR;

namespace Application.CQRS.Account.Commands.CreateAccount;

public sealed record CreateAccountCommand(
    string Name,
    string Surname,
    string Email,
    string Password,
    string RepeatPassword
    ) : IRequest<AccountResponse>;