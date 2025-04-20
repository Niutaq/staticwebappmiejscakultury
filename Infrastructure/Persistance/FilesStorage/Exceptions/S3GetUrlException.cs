using Domain.Exceptions;

namespace Infrastructure.Persistance.FilesStorage.Exceptions;

public class S3GetUrlException : BaseException
{
    public S3GetUrlException(string errorCode) : base($"Nieoczekiwany błąd podczas pobrania url {errorCode}") { }
}