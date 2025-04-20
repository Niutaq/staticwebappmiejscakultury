namespace Application.CQRS.Comment.Response;

public record AddCommentResponse(string Message, Guid CommentId);