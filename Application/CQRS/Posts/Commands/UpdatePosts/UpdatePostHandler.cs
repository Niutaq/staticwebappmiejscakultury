using Application.CQRS.Posts.Responses;
using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.PostsInterfaces;
using MediatR;

namespace Application.CQRS.Posts.Commands.UpdatePosts;

public class UpdatePostHandler : IRequestHandler<UpdatePostCommand, UpdatePostResponse>
{
    private readonly IPostsRepository _postsRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdatePostHandler(IPostsRepository postsRepository, ICurrentUserService currentUserService)
    {
        _postsRepository = postsRepository;
        _currentUserService = currentUserService;
    }
    
    public async Task<UpdatePostResponse> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        await _postsRepository.UpdatePostAsync(userId, request.PostId, request.NewCategory, request.NewTitle, request.NewDescription, request.NewLocalizationX, request.NewLocalizationY, cancellationToken);

        return new UpdatePostResponse("Aktualizacja przebiegła pomyślnie");
    }
}