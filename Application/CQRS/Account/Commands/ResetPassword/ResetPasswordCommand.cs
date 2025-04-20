using Application.CQRS.Account.Responses;
using MediatR;

namespace Application.CQRS.Account.Commands.ResetPassword;

public record ResetPasswordCommand(
    string Token,
    Guid UserId,
    string Password,
    string ConfirmedPassword
    ) : IRequest<AccountResponse>;