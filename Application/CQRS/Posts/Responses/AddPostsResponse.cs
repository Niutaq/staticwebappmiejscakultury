namespace Application.CQRS.Posts.Responses;

public record AddPostsResponse(string Message, Guid PostId);