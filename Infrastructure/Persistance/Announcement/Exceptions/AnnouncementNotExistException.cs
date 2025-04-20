using Domain.Exceptions;

namespace Infrastructure.Persistance.Announcement.Exceptions;

public class AnnouncementNotExistException : BaseException
{
    public AnnouncementNotExistException() : base("Wybierz poprawne ogłoszenie!") { }
}