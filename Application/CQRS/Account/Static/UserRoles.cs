using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Application.CQRS.Account.Static;

public static class UserRoles
{
    public const string User = nameof(User);
    public const string Admin = nameof(Admin);
    
    private static List<IdentityRole<Guid>> Roles;

    static UserRoles()
    {
        Roles = new List<IdentityRole<Guid>>()
        {
            new(User),
            new(Admin)
        };
    }

    public static List<IdentityRole<Guid>> Get()
    {
        return Roles;
    }
}