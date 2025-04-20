using Application.Persistance.Interfaces.S3StorageInterfaces;
using MediatR;

namespace Application.CQRS.Image.Queries;

public class GetUserAvatarHandler : IRequestHandler<GetUserAvatarQuery, string>
{
    private readonly IS3StorageService _s3StorageService;

    public GetUserAvatarHandler(IS3StorageService s3StorageService)
    {
        _s3StorageService = s3StorageService;
    }
    
    public async Task<string> Handle(GetUserAvatarQuery request, CancellationToken cancellationToken)
    {
        var url = _s3StorageService.GetFileUrl(request.S3Key);

        return url;
    }
}