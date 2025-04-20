namespace Domain.Exceptions.MessagesExceptions;

public class UserWithEmailExistsException : BaseException
{
    public UserWithEmailExistsException() : base("E-mail jest już zajęty")
    {
        
    }
}