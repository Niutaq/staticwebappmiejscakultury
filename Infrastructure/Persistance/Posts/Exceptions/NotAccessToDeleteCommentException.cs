using Domain.Exceptions;

namespace Infrastructure.Persistance.Posts.Exceptions;

public class NotAccessToDeleteCommentException : BaseException
{
    public NotAccessToDeleteCommentException() : base("Nie możesz usunąc tego komentarza!") { }
}