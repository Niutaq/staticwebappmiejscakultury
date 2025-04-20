namespace Domain.Exceptions.MessagesExceptions;

public class UserNotFoundException : BaseException
{
    public UserNotFoundException() : base("Nie znaleziono użytkownika") { }
}