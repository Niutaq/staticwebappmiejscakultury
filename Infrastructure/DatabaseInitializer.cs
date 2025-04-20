using Application.CQRS.Account.Static;
using Domain.Entities;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Seeders.Roles;
using Infrastructure.Persistance.Seeders.UserSeeder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure;

public sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetService(typeof(MiejscaKulturyDbContext)) as MiejscaKulturyDbContext;

        var roleManager =
            scope.ServiceProvider.GetService(typeof(RoleManager<IdentityRole<Guid>>)) as
                RoleManager<IdentityRole<Guid>>;
        var userManager = scope.ServiceProvider.GetService(typeof(UserManager<Users>)) as UserManager<Users>;

        if (context is not null)
        {
            await context.Database.MigrateAsync(cancellationToken);
            
            await UserRolesSeeder.SeedAsync(roleManager, context, cancellationToken);
            await AdminSeeder.SeedAssync(userManager, context, cancellationToken);
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}