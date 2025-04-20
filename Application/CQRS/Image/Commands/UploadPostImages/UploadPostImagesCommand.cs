using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Image.Commands.UploadPostImages;

public record UploadPostImagesCommand(
    IFormFile File,
    Guid Id
    ) : IRequest;