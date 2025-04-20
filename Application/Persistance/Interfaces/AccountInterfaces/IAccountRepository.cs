using System.Security.Claims;
using Application.CQRS.Account.DTO;
using Domain.Authentication;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace Application.Persistance.Interfaces.AccountInterfaces;

public interface IAccountRepository
{
    Task<Guid> CreateUserAsync(string email, string password, string name, string surname, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    Task<Users> FindUserAsync(string email, CancellationToken cancellationToken);
    JsonWebToken GenerateJwtToken(Guid userId, string email, ICollection<string> roles, ICollection<Claim> claims);
    Task<Users?> GetUserById(Guid id, CancellationToken cancellationToken);
    Task<string> GenerateEmailConfirmTokenAsync(Users user, CancellationToken cancellationToken);
    Task ConfirmAccountAsync(Guid userId, string token, CancellationToken cancellationToken);
    Task<ResetPasswordDto> GeneratePasswordTokenAsync(string email, CancellationToken cancellationToken);
    Task ResetPasswordAssync(string token, Guid userId, string password, CancellationToken cancellationToken);
    Task UpdateUserImageAsync(Users user, CancellationToken cancellationToken);
    Task<Avatarimage> GetImageKeyAsync(Guid userId, CancellationToken cancellationToken);
    Task DeleteUserImageAsync(Avatarimage avatarImage, CancellationToken cancellationToken);
    Task AddAdminRoleAsync(string email, CancellationToken cancellationToken);
}