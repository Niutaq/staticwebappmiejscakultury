using Application.CQRS.Announcement.Dtos;
using Application.CQRS.Announcement.Enum;
using Application.CQRS.Announcement.Extension;
using Application.Persistance.Interfaces.AnnouncementInterfaces;
using Infrastructure.Persistance.Announcement.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Announcement.Repository;

public class AnnouncementRepository : IAnnouncementRepository
{
    private readonly MiejscaKulturyDbContext _context;

    public AnnouncementRepository(MiejscaKulturyDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAnnouncementAsync(Domain.Entities.Announcement announcement, CancellationToken cancellationToken)
    {
        await _context.AddAsync(announcement, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAnnouncementAsync(Guid userId, Guid announcementId, CancellationToken cancellationToken)
    {
        var announcemet = await _context.Announcements.FindAsync(announcementId);

        if (announcemet is null) throw new AnnouncementNotExistException();

        _context.Announcements.Remove(announcemet);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<GetAnnouncementDto>> GetAnnouncementsAsync(State state, CancellationToken cancellationToken)
    {
        var announcements = new List<Domain.Entities.Announcement>();
        if (state == State.Old)
        {
            announcements =
                await _context.Announcements
                    .Where(x => x.Date < DateOnly.FromDateTime(DateTime.UtcNow))
                    .OrderByDescending(x => x.Date)
                    .ToListAsync(cancellationToken);
        }
        else if (state == State.NowaDay)
        {
            announcements =
                await _context.Announcements
                    .Where(x => x.Date == DateOnly.FromDateTime(DateTime.UtcNow))
                    .ToListAsync(cancellationToken);
        }
        else if (state == State.Future)
        {
            announcements =
                await _context.Announcements
                    .Where(x => x.Date > DateOnly.FromDateTime(DateTime.UtcNow))
                    .OrderBy(x => x.Date)
                    .ToListAsync(cancellationToken);
        }
        else
        {
            announcements = await _context.Announcements.ToListAsync(cancellationToken);
        }

        return announcements.Select(x => x.GetAnnouncementAsDto()).ToList();
    }
}