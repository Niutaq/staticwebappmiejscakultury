using Application.CQRS.Account.Responses;
using MediatR;

namespace Application.CQRS.Account.Commands.AddAdminRole;

public record AddAdminRoleCommand(
    string Email
    ) : IRequest<AccountResponse>;