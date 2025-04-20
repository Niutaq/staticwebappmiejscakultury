using Application.CQRS.Comment.Dtos;
using MediatR;

namespace Application.CQRS.Comment.Queries.DisplayComments;

public record DisplayCommentsQuery(
    Guid PostId
    ) : IRequest<List<CommentDto>>;