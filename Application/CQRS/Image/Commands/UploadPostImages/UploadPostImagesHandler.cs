using Application.Persistance.Interfaces.S3StorageInterfaces;
using Domain.Entities;
using MediatR;

namespace Application.CQRS.Image.Commands.UploadPostImages;

public class UploadPostImagesHandler : IRequestHandler<UploadPostImagesCommand>
{
    private readonly IS3StorageService _s3StorageService;

    public UploadPostImagesHandler(IS3StorageService s3StorageService)
    {
        _s3StorageService = s3StorageService;
    }
    
    public async Task Handle(UploadPostImagesCommand request, CancellationToken cancellationToken)
    {
        var uploadImage = await _s3StorageService.UploadFileAsync(request.File, cancellationToken);
        var url = _s3StorageService.GetFileUrl(uploadImage);

        var image = new Postimage
        {
            Name = request.File.Name,
            ContentType = request.File.ContentType,
            TotalBytes = request.File.Length,
            S3Key = uploadImage,
            Url = url,
            PlacesId = request.Id
        };

        await _s3StorageService.SavePostImageAsync(image, cancellationToken);
    }
}