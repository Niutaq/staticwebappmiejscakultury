using Domain.Exceptions;

namespace Infrastructure.Persistance.Posts.Exceptions;

public class PostNotFoundException : BaseException
{
    public PostNotFoundException() : base("Nie można dodać komentarza do nie istniejącego posta!") { }
}