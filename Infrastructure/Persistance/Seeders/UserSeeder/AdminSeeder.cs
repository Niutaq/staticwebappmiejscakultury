using System.Security.Claims;
using Application.CQRS.Account.Static;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.MessagesExceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistance.Seeders.UserSeeder;

public class AdminSeeder
{
    private const string Email = "admin@admin.com";
    private const string Password = "ZAQ!2wsxCDE#4rfv";
    private const string Name = "Admin";

    internal static async Task SeedAssync(UserManager<Users> userManager, MiejscaKulturyDbContext context, CancellationToken cancellationToken)
    {
        var isUserExist = await userManager.Users.AnyAsync(x => x.Email == Email, cancellationToken);
        if(isUserExist) return;

        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);

        var user = new Users()
        {
            Email = Email,
            UserName = Email,
            Name = Name,
            Surname = Name
        };

        var createdUser = await userManager.CreateAsync(user, Password);
        if (!createdUser.Succeeded) throw new CreateUserException(createdUser.Errors);

        var addUserRole = await userManager.AddToRoleAsync(user, UserRoles.User);
        if (!addUserRole.Succeeded) throw new AddToRoleException();

        var addAdminRole = await userManager.AddToRoleAsync(user, UserRoles.Admin);
        if (!addAdminRole.Succeeded) throw new AddToRoleException();

        var addEmailClaim = await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Email, user.Email));
        if (!addEmailClaim.Succeeded) throw new AddClaimException();

        var addIdentifier =
            await userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        if (!addIdentifier.Succeeded) throw new AddClaimException();

        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }
}