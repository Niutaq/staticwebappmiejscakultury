namespace Domain.Exceptions.MessagesExceptions;

public class AddToRoleException : BaseException
{
    public AddToRoleException() : base("Nie udało się dodać roli.") { }
}