using Application.CQRS.Comment.Response;
using MediatR;

namespace Application.CQRS.Comment.Commands.DeleteComment;

public record DeleteCommentCommand(
    Guid CommentId
    ) : IRequest<DeleteCommentResponse>;