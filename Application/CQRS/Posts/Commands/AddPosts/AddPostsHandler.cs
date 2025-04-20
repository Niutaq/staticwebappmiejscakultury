using Application.CQRS.Image.Commands.UploadPostImages;
using Application.CQRS.Posts.Responses;
using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.PostsInterfaces;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Posts.Commands.AddPosts;

public class AddPostsHandler : IRequestHandler<AddPostsCommand, AddPostsResponse>
{
    private readonly IPostsRepository _postsRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;

    public AddPostsHandler(IPostsRepository postsRepository, ICurrentUserService currentUserService, IMediator mediator)
    {
        _postsRepository = postsRepository;
        _currentUserService = currentUserService;
        _mediator = mediator;
    }
    
    public async Task<AddPostsResponse> Handle(AddPostsCommand request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        
        var post = new Places
        {
            Title = request.Title,
            Description = request.Description,
            Category = request.Category,
            LocalizationX = request.LocalizationX,
            LocalizationY = request.LocalizationY,
            UsersId = userId
        };
        
        await _postsRepository.AddPostAsync(post, cancellationToken);

        foreach (var photo in request.Photos)
        { 
            await _mediator.Send(new UploadPostImagesCommand(photo, post.Id), cancellationToken);
        }
        
        return new AddPostsResponse("Pomyślnie dodano post", post.Id);
    }
}