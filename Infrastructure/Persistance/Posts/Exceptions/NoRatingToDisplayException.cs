using Domain.Exceptions;

namespace Infrastructure.Persistance.Posts.Exceptions;

public class NoRatingToDisplayException:BaseException
{
    public NoRatingToDisplayException():base ("Nie ma takiej oceny do wyświetlenia!"){}
}