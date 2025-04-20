using Application.CQRS.Image.Queries;
using Application.Persistance.Interfaces.AccountInterfaces;
using Domain.Authentication;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.MessagesExceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Application.CQRS.Account.Commands.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, JsonWebToken>
{
    private readonly SignInManager<Users> _signInManager;
    private readonly UserManager<Users> _userManager;
    private readonly IAccountRepository _accountRepository;
    private readonly IMediator _mediator;

    public SignInCommandHandler(SignInManager<Users> signInManager, UserManager<Users> userManager, IAccountRepository accountRepository, IMediator mediator)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _accountRepository = accountRepository;
        _mediator = mediator;
    }

    public async Task<JsonWebToken> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _accountRepository.FindUserAsync(request.Email, cancellationToken);

        if (user is null) throw new InvalidCredentialsException();

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);
        if (!result.Succeeded) throw new InvalidCredentialsException();

        var userRoles = await _userManager.GetRolesAsync(user);
        var userClaims = await _userManager.GetClaimsAsync(user);

        var jwt = _accountRepository.GenerateJwtToken(user.Id, user.Email, userRoles, userClaims);
        
        jwt.Name = user.Name;
        jwt.Surname = user.Surname;

        if (user.AvatarimageId != null)
        {
            jwt.AvatarUrl = await _mediator.Send(new GetUserAvatarQuery(user.Avatarimage.S3Key), cancellationToken);
        }
        
        return jwt;
    }
}