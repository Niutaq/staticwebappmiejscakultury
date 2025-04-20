using System.Text;
using Application.CQRS.Account.Commands.SignIn;
using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.AnnouncementInterfaces;
using Application.Persistance.Interfaces.EmailInterfaces;
using Application.Persistance.Interfaces.PostsInterfaces;
using Application.Persistance.Interfaces.S3StorageInterfaces;
using Domain.Authentication;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Persistance;
using Infrastructure.Persistance.Account.AccountRepositories;
using Infrastructure.Persistance.Announcement.Repository;
using Infrastructure.Persistance.FilesStorage.Configuration;
using Infrastructure.Persistance.FilesStorage.Services;
using Infrastructure.Persistance.Posts.PostsRepositories;
using Infrastructure.Persistance.Repositories.Email.Configuration;
using Infrastructure.Persistance.Repositories.Email.EmailRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,ConfigurationManager configuration)
    {
        services.AddDbContext<MiejscaKulturyDbContext>(options => options.UseNpgsql(
            configuration.GetConnectionString("Database"),
            m => m.MigrationsAssembly(typeof(AssemblyReference).Assembly.ToString())));

        services.AddHostedService<DatabaseInitializer>();
        
        services.AddIdentity<Users, IdentityRole<Guid>>()
            .AddEntityFrameworkStores<MiejscaKulturyDbContext>()
            .AddDefaultTokenProviders();

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredUniqueChars = 4;

            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
            options.Lockout.MaxFailedAccessAttempts = 10;
            options.Lockout.AllowedForNewUsers = false;

            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        });
        
        services.AddScoped<SignInManager<Users>>();
        services.AddScoped<UserManager<Users>>();
        

        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<IEmailSenderService, EmailSenderService>();
        services.AddScoped<IS3StorageService, S3StorageService>();
        services.AddScoped<IPostsRepository, PostsRepository>();
        services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();

        var smtpConfig = new SmtpConfig();
        configuration.GetSection("SMTP").Bind(smtpConfig);
        services.AddSingleton(smtpConfig);

        var s3Config = new S3Configuration();
        configuration.GetSection("S3Service").Bind(s3Config);
        services.AddSingleton(s3Config);

        services.AddHttpContextAccessor();
        services.AddValidatorsFromAssemblyContaining<SignInCommand>();
        
        return services;
    }
    
    public static IServiceCollection AddAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));
        
        var authenticationSettings = new JwtSettings();
        
        configuration.GetSection(JwtSettings.SectionName).Bind(authenticationSettings);
        services.AddSingleton(authenticationSettings);
        
        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(cfg =>
        {
            cfg.RequireHttpsMetadata = false;
            cfg.SaveToken = true;
            cfg.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = authenticationSettings.Issuer,
                ValidAudience = authenticationSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.Secret))
            };
        });

        return services;
    }
}