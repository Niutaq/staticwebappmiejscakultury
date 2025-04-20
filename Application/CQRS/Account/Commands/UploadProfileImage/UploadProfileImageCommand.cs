using Application.CQRS.Account.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.CQRS.Account.Commands.UploadProfileImage;

public sealed record UploadProfileImageCommand(IFormFile Photo) : IRequest<AccountResponse>;