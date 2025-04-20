using Domain.Exceptions;

namespace Infrastructure.Persistance.Posts.Exceptions;

public class HasNoAccessException : BaseException
{
    public HasNoAccessException() : base("Nie możesz edytować tego posta!") { }
}