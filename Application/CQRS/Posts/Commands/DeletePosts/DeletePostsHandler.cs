using Application.CQRS.Posts.Responses;
using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.PostsInterfaces;
using MediatR;

namespace Application.CQRS.Posts.Commands.DeletePosts;

public class DeletePostsHandler : IRequestHandler<DeletePostsCommand, DeletePostsResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IPostsRepository _postsRepository;

    public DeletePostsHandler(ICurrentUserService currentUserService, IPostsRepository postsRepository)
    {
        _currentUserService = currentUserService;
        _postsRepository = postsRepository;
    }
    
    public async Task<DeletePostsResponse> Handle(DeletePostsCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        await _postsRepository.DeletePostAsync(userId, request.PostId, cancellationToken);

        return new DeletePostsResponse("Poprawnie usunięto!");
    }
}