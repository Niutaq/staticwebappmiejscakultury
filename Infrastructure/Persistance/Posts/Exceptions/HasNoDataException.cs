using Domain.Exceptions;

namespace Infrastructure.Persistance.Posts.Exceptions;

public class HasNoDataException : BaseException
{
    public HasNoDataException() : base("Podany post nie istnieje!") { }
}