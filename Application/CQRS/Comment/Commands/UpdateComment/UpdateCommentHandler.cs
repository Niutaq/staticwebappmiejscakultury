using Application.CQRS.Comment.Response;
using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.PostsInterfaces;
using MediatR;

namespace Application.CQRS.Comment.Commands.UpdateComment;

public class UpdateCommentHandler : IRequestHandler<UpdateCommentCommand, UpdateCommentResponse>
{
    private readonly IPostsRepository _postsRepository;
    private readonly ICurrentUserService _currentUserService;

    public UpdateCommentHandler(IPostsRepository postsRepository, ICurrentUserService currentUserService)
    {
        _postsRepository = postsRepository;
        _currentUserService = currentUserService;
    }
    
    public async Task<UpdateCommentResponse> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;

        await _postsRepository.UpdateCommentAsync(userId, request.CommentId, request.NewMessage, cancellationToken);

        return new UpdateCommentResponse("Zaktualizowano komentarz!");
    }
}