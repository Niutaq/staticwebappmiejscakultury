using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Application.Persistance.Interfaces.S3StorageInterfaces;

public interface IS3StorageService
{
    Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken);
    String GetFileUrl(string fileKey);
    Task<MemoryStream> GetFileAsync(string fileKey, CancellationToken cancellationToken);
    Task<Guid> SaveChangesAsync(Avatarimage avatarImage, CancellationToken cancellationToken);
    Task DeleteFileAsync(string s3Key, CancellationToken cancellationToken);
    Task SavePostImageAsync(Postimage image, CancellationToken cancellationToken);
}