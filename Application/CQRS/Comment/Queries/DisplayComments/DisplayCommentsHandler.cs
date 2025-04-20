using Application.CQRS.Comment.Dtos;
using Application.Persistance.Interfaces.PostsInterfaces;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Comment.Queries.DisplayComments;

public class DisplayCommentsHandler : IRequestHandler<DisplayCommentsQuery, List<CommentDto>>
{
    private readonly IPostsRepository _postsRepository;

    public DisplayCommentsHandler(IPostsRepository postsRepository)
    {
        _postsRepository = postsRepository;
    }
    
    public async Task<List<CommentDto>> Handle(DisplayCommentsQuery request, CancellationToken cancellationToken)
    {
        return await _postsRepository.DisplayCommentAsync(request.PostId, cancellationToken);
    }
}