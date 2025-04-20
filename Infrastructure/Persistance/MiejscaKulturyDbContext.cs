using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance;

public class MiejscaKulturyDbContext : IdentityDbContext<Users, IdentityRole<Guid>, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
{
    public MiejscaKulturyDbContext(DbContextOptions<MiejscaKulturyDbContext> options): base(options){}
    
    public DbSet<Places> Place { get; set; }
    public DbSet<Avatarimage> AvagarImages { get; set; }
    public DbSet<Postimage> PostImages { get; set; }
    public DbSet<Comments> Comment { get; set; }
    public DbSet<Domain.Entities.Announcement> Announcements { get; set; }
    
    public DbSet<Ratings> Rating { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}