using Domain.Exceptions;

namespace Infrastructure.Persistance.Posts.Exceptions;

public class NotAccessToCommentException : BaseException
{
    public NotAccessToCommentException() : base("Nie można edytować tego komentarza!") { }
}