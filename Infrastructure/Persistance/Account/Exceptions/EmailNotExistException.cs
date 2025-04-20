using Domain.Exceptions;

namespace Infrastructure.Persistance.Account.Exceptions;

public class EmailNotExistException : BaseException
{
    public EmailNotExistException() : base("Taki użytkownik nie istnieje!") { }
}