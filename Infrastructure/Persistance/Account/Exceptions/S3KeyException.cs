using Domain.Exceptions;

namespace Infrastructure.Persistance.Account.Exceptions;

public class S3KeyException : BaseException
{
    public S3KeyException() : base("S3Key nie istnieje!") { }
}