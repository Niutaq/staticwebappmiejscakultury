using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.CQRS.Account.DTO;
using Application.CQRS.Account.Static;
using Application.Persistance.Interfaces.AccountInterfaces;
using Domain.Authentication;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.MessagesExceptions;
using Infrastructure.Persistance.Account.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Persistance.Account.AccountRepositories;

public class AccountRepository : IAccountRepository
{
    private readonly MiejscaKulturyDbContext _context;
    private readonly JwtSettings _jwtSettings;
    private readonly UserManager<Users> _userManager;

    public AccountRepository(MiejscaKulturyDbContext context, JwtSettings jwtSettings, UserManager<Users> userManager)
    {
        _context = context;
        _jwtSettings = jwtSettings;
        _userManager = userManager;
    }

    public async Task<Guid> CreateUserAsync(string email, string password, string name, string surname, CancellationToken cancellationToken)
    {
        var isEmailExist = await _userManager.Users.AnyAsync(x => x.Email == email, cancellationToken);
        if (isEmailExist) throw new UserWithEmailExistsException();

        var user = new Users
        {
            Email = email,
            UserName = email,
            Name = name,
            Surname = surname
        };

        var createUser = await _userManager.CreateAsync(user, password);
        if (!createUser.Succeeded) throw new CreateUserException(createUser.Errors);

        var addUserRole = await _userManager.AddToRoleAsync(user, UserRoles.User);
        if (!addUserRole.Succeeded) throw new AddToRoleException();

        var addNameClaim = await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
        if (!addNameClaim.Succeeded) throw new AddClaimException();

        return user.Id;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Users> FindUserAsync(string email, CancellationToken cancellationToken)
    {
        return await _context.Users.Include(x => x.Avatarimage)
            .FirstOrDefaultAsync(x => x.Email == email, cancellationToken);
    }

    public JsonWebToken GenerateJwtToken(Guid userId, string email, ICollection<string> roles, ICollection<Claim> claims)
    {
        var now = System.DateTime.UtcNow;
        
        var jwtClaims = new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new(JwtRegisteredClaimNames.UniqueName, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString())
        };

        if (roles?.Any() is true)
        {
            foreach (var role in roles)
            {
                jwtClaims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        if (claims?.Any() is true)
        {
            var customClaims = new List<Claim>();
            foreach (var claim in claims)
            {
                customClaims.Add(new Claim(claim.Type, claim.Value));
            }
            jwtClaims.AddRange(customClaims);
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = now.AddMinutes(_jwtSettings.ExpiryMinutes);

        var jwt = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            jwtClaims,
            expires: expires,
            signingCredentials: cred
            );

        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new JsonWebToken
        {
            AccessToken = token,
            Expires = new DateTimeOffset(expires).ToUnixTimeSeconds(),
            UserId = userId,
            Email = email,
            Roles = roles,
            Claims = claims?.ToDictionary(x => x.Type, c => c.Value)
        };
    }

    public async Task<Users?> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var user =  await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken)
            ?? throw new UserNotFoundException();

        return user;
    }

    public async Task<string> GenerateEmailConfirmTokenAsync(Users user, CancellationToken cancellationToken)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task ConfirmAccountAsync(Guid userId, string token, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken)
            ?? throw new ConfirmAccountException();

        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded) throw new ConfirmAccountException();
    }

    public async Task<ResetPasswordDto> GeneratePasswordTokenAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);

        if (user is null) throw new UserNotFoundException();

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        return new ResetPasswordDto
        {
            UserId = user.Id,
            Token = token
        };
    }

    public async Task ResetPasswordAssync(string token, Guid userId, string password, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);
        if (user is null) throw new UserNotFoundException();

        var result = await _userManager.ResetPasswordAsync(user, token, password);
        if (!result.Succeeded) throw new CreateUserException(result.Errors);
    }

    public async Task UpdateUserImageAsync(Users user, CancellationToken cancellationToken)
    {
        await _userManager.UpdateAsync(user);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Avatarimage> GetImageKeyAsync(Guid userId, CancellationToken cancellationToken)
    {
        var S3Key = await _userManager.Users.Include(u => u.Avatarimage)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (S3Key.Avatarimage.S3Key is null) throw new S3KeyException();

        return S3Key.Avatarimage;
    }

    public async Task DeleteUserImageAsync(Avatarimage avatarImage, CancellationToken cancellationToken)
    {
        _context.AvagarImages.Remove(avatarImage);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AddAdminRoleAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken)
                   ?? throw new EmailNotExistException();

        var addUserRole = await _userManager.AddToRoleAsync(user, UserRoles.Admin);
        if (!addUserRole.Succeeded) throw new AddToRoleException();
    }
}