using Application.CQRS.Account.Responses;
using MediatR;

namespace Application.CQRS.Account.Events.SendResetPasswordEmail;

public sealed record SendResetPasswordEmailEvent(string Email ) : IRequest<AccountResponse>;