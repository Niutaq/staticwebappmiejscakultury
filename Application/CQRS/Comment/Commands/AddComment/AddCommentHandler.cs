using Application.CQRS.Comment.Response;
using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.PostsInterfaces;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Comment.Commands.AddComment;

public class AddCommentHandler : IRequestHandler<AddCommentCommand, AddCommentResponse>
{
    private readonly IPostsRepository _postsRepository;
    private readonly ICurrentUserService _currentUserService;

    public AddCommentHandler(IPostsRepository postsRepository, ICurrentUserService currentUserService)
    {
        _postsRepository = postsRepository;
        _currentUserService = currentUserService;
    }
    
    public async Task<AddCommentResponse> Handle(AddCommentCommand request, CancellationToken cancellationToken)
    {
        await _postsRepository.IsPostExistAsync(request.PlaceId, cancellationToken);
        
        var userId = _currentUserService.UserId;
        var now = DateTimeOffset.UtcNow;

        var newComment = new Comments
        {
            DateAdded = now,
            Message = request.Message,
            UsersId = userId,
            PlacesId = request.PlaceId
        };

        await _postsRepository.AddCommentAsync(newComment, cancellationToken);

        return new AddCommentResponse("Pomyślnie dodano komentarz!", newComment.Id);
    }
}