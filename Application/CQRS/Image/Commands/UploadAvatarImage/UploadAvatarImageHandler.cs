using Application.CQRS.Image.Responses;
using Application.Persistance.Interfaces.S3StorageInterfaces;
using MediatR;

namespace Application.CQRS.Image.Commands.UploadAvatarImage;

public class UploadAvatarImageHandler : IRequestHandler<UploadAvatarImageCommand, UploadImageResponse>
{
    private readonly IS3StorageService _s3StorageService;

    public UploadAvatarImageHandler(IS3StorageService storageService)
    {
        _s3StorageService = storageService;
    }
    
    public async Task<UploadImageResponse> Handle(UploadAvatarImageCommand request, CancellationToken cancellationToken)
    {
        var uploadResult = await _s3StorageService.UploadFileAsync(request.Image, cancellationToken);
        var url = _s3StorageService.GetFileUrl(uploadResult);

        var image = new Domain.Entities.Avatarimage
        {
            Name = request.Image.Name,
            ContentType = request.Image.ContentType,
            TotalBytes = request.Image.Length,
            S3Key = uploadResult,
            Url = url,
        };
        

        var id = await _s3StorageService.SaveChangesAsync(image, cancellationToken);
        return new UploadImageResponse(id , image);
    }
}