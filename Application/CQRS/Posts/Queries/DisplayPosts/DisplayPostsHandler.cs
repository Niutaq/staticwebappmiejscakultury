using Application.CQRS.Posts.Dtos;
using Application.Persistance.Interfaces.PostsInterfaces;
using MediatR;

namespace Application.CQRS.Posts.Queries.DisplayPosts;

public class DisplayPostsHandler : IRequestHandler<DisplayPostsQuery, List<DisplayPostsDto>>
{
    private readonly IPostsRepository _postsRepository;

    public DisplayPostsHandler(IPostsRepository postsRepository)
    {
        _postsRepository = postsRepository;
    }
    
    public async Task<List<DisplayPostsDto>> Handle(DisplayPostsQuery request, CancellationToken cancellationToken)
    {
        return await _postsRepository.DisplayPostsAsync(request.Category, cancellationToken);
    }
}