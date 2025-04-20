using Domain.Exceptions;

namespace Infrastructure.Persistance.Posts.Exceptions;

public class NoRattingToUpdateException:BaseException
{
    public NoRattingToUpdateException():base ("Nie można zaktualizować tej oceny"){}
}