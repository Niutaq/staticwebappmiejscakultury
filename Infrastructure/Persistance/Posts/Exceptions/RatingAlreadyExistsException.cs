using Domain.Exceptions;

namespace Infrastructure.Persistance.Posts.Exceptions;

public class RatingAlreadyExistsException: BaseException
{
    public RatingAlreadyExistsException() : base ("Ocena została dodana wcześniej"){}
}