using Application.CQRS.Comment.Response;
using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.PostsInterfaces;
using MediatR;

namespace Application.CQRS.Comment.Commands.DeleteComment;

public class DeleteCommentHandler : IRequestHandler<DeleteCommentCommand, DeleteCommentResponse>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IPostsRepository _postsRepository;

    public DeleteCommentHandler(ICurrentUserService currentUserService, IPostsRepository postsRepository)
    {
        _currentUserService = currentUserService;
        _postsRepository = postsRepository;
    }
    
    public async Task<DeleteCommentResponse> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        var userRoles = _currentUserService.UserRoles();

        await _postsRepository.DeleteCommentAsync(userId, userRoles, request.CommentId, cancellationToken);

        return new DeleteCommentResponse("Pomyślnie usunięto komentarz!");
    }
}