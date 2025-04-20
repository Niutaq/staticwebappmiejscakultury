namespace Domain.Exceptions.MessagesExceptions;

public class ConfirmAccountException : BaseException
{
    public ConfirmAccountException() : base("Weryfikacja nie powiodła się!") { }
}