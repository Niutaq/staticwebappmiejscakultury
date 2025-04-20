using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Entities;

public class Users : IdentityUser<Guid>
{
    public string Name { get; set; }
    public string Surname { get; set; }

    public Guid? AvatarimageId { get; set; }
    public Avatarimage Avatarimage { get; set; }
    
    public List<Comments> comments { get; set; }
    
}

public class UserConfiguration : IEntityTypeConfiguration<Users>
{
    public void Configure(EntityTypeBuilder<Users> builder)
    {
        
    }
}
