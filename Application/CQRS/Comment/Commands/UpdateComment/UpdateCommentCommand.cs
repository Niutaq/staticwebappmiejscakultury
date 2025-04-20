using Application.CQRS.Comment.Response;
using MediatR;

namespace Application.CQRS.Comment.Commands.UpdateComment;

public record UpdateCommentCommand(
    Guid CommentId,
    string NewMessage
    ) : IRequest<UpdateCommentResponse>;