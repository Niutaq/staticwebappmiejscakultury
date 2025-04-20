using MediatR;

namespace Application.CQRS.Image.Commands.DeleteImage;

public record DeleteImageCommand(
    string S3Key
    ) : IRequest;