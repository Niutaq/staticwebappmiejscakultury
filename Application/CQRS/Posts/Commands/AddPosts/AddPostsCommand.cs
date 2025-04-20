using Application.CQRS.Posts.Responses;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.CQRS.Posts.Commands.AddPosts;

public record AddPostsCommand(
    IFormFileCollection Photos,
    string Title,
    string Description,
    PlacesCategory Category,
    double LocalizationX,
    double LocalizationY
    ) : IRequest<AddPostsResponse>;