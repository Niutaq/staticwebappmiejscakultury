namespace Domain.Exceptions.MessagesExceptions;

public class InvalidCredentialsException : BaseException
{
    public InvalidCredentialsException() : base("Niepoprawne dane logowania!")
    {
        
    }
}