using Application.CQRS.Image.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Image.Commands.UploadAvatarImage;

public record UploadAvatarImageCommand(
    IFormFile Image
    ) : IRequest<UploadImageResponse>;