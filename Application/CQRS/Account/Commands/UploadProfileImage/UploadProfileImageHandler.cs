using Application.CQRS.Account.Responses;
using Application.CQRS.Image.Commands.DeleteImage;
using Application.CQRS.Image.Commands.UploadAvatarImage;
using Application.Persistance.Interfaces.AccountInterfaces;
using MediatR;

namespace Application.CQRS.Account.Commands.UploadProfileImage;

public sealed class UploadProfileImageHandler : IRequestHandler<UploadProfileImageCommand, AccountResponse>
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMediator _mediator;
    private readonly ICurrentUserService _currentUserService;

    public UploadProfileImageHandler(IAccountRepository accountRepository, IMediator mediator, ICurrentUserService currentUserService)
    {
        _accountRepository = accountRepository;
        _mediator = mediator;
        _currentUserService = currentUserService;
    }
    
    public async Task<AccountResponse> Handle(UploadProfileImageCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        var user = await _accountRepository.GetUserById(userId, cancellationToken);

        if (user.AvatarimageId != null)
        {
            var image = await _accountRepository.GetImageKeyAsync(userId, cancellationToken);
            await _mediator.Send(new DeleteImageCommand(image.S3Key), cancellationToken);
            await _accountRepository.DeleteUserImageAsync(image, cancellationToken);
        }
        
        var fileResult = await _mediator.Send(new UploadAvatarImageCommand(request.Photo), cancellationToken);

        user.AvatarimageId = fileResult.Id;
        await _accountRepository.UpdateUserImageAsync(user, cancellationToken);

        return new AccountResponse(fileResult.AvatarImage.Url);
    }
}