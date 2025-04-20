using Application.CQRS.Posts.Responses;
using MediatR;

namespace Application.CQRS.Posts.Commands.LikePosts;

public sealed record LikePostCommand(
    Guid PostId
) : IRequest<LikePostResponse>;