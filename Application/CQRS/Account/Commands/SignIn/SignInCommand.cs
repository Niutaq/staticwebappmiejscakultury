using Domain.Authentication;
using MediatR;

namespace Application.CQRS.Account.Commands.SignIn;

public record SignInCommand(
    string Email,
    string Password
    ) : IRequest<JsonWebToken>;