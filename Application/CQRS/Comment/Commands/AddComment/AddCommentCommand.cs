using Application.CQRS.Comment.Response;
using MediatR;

namespace Application.CQRS.Comment.Commands.AddComment;

public record AddCommentCommand(
    Guid PlaceId,
    string Message
    ) : IRequest<AddCommentResponse>;