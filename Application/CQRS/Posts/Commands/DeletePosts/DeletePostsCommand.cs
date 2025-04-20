using Application.CQRS.Posts.Responses;
using MediatR;

namespace Application.CQRS.Posts.Commands.DeletePosts;

public record DeletePostsCommand(
    Guid PostId
    ) : IRequest<DeletePostsResponse>;