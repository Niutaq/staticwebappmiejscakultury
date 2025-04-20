using Domain.Exceptions;

namespace Infrastructure.Persistance.FilesStorage.Exceptions;

public class S3UploadException : BaseException
{
    public S3UploadException(string errorCode) : base($"Wystąpił błąd: {errorCode}") { }
}