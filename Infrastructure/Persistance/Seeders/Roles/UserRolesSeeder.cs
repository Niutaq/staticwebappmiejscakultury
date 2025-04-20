using Application.CQRS.Account.Static;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Persistance.Seeders.Roles;

public static class UserRolesSeeder
{
    public static async Task SeedAsync(RoleManager<IdentityRole<Guid>> roleManager, MiejscaKulturyDbContext context, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var roles = UserRoles.Get();
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role.Name))
            {
                await roleManager.CreateAsync(role);
            }
        }

        await transaction.CommitAsync(cancellationToken);
    }
}