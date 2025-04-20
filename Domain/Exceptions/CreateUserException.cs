using Microsoft.AspNetCore.Identity;

namespace Domain.Exceptions;

public class CreateUserException : BaseException
{
    public CreateUserException() : base("Wystąpiło kilka błędów.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public CreateUserException(IEnumerable<IdentityError> errors) : this()
    {
        Errors = errors.GroupBy(e => e.Code, e => e.Description)
            .ToDictionary(a => a.Key, a => a.ToArray());
    }
    
    public IDictionary<string, string[]> Errors { get; }
}