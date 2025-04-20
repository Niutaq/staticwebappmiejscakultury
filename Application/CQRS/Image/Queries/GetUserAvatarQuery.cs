using MediatR;

namespace Application.CQRS.Image.Queries;

public record GetUserAvatarQuery(string S3Key) : IRequest<string>;