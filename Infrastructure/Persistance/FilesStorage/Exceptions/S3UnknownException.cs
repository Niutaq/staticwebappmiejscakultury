using Domain.Exceptions;

namespace Infrastructure.Persistance.FilesStorage.Exceptions;

public class S3UnknownException : BaseException
{
    public S3UnknownException() : base("Coś poszło nie tak z połączeniem do S3Service") { }
}