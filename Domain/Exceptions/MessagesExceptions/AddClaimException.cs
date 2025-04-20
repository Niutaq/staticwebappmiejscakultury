namespace Domain.Exceptions.MessagesExceptions;

public class AddClaimException : BaseException
{
    public AddClaimException() : base("Nie dodano claimu!") { }
}