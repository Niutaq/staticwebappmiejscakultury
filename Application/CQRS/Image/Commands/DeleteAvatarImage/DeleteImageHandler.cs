using Application.Persistance.Interfaces.S3StorageInterfaces;
using MediatR;

namespace Application.CQRS.Image.Commands.DeleteImage;

public class DeleteImageHandler : IRequestHandler<DeleteImageCommand>
{
    private readonly IS3StorageService _s3StorageService;

    public DeleteImageHandler(IS3StorageService s3StorageService)
    {
        _s3StorageService = s3StorageService;
    }
    
    public async Task Handle(DeleteImageCommand request, CancellationToken cancellationToken)
    {
        await _s3StorageService.DeleteFileAsync(request.S3Key, cancellationToken);
    }
}