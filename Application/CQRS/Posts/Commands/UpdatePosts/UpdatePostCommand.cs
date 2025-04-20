using Application.CQRS.Posts.Responses;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Posts.Commands.UpdatePosts;

public record UpdatePostCommand(
    Guid PostId,
    PlacesCategory NewCategory,
    string NewTitle,
    string NewDescription,
    double NewLocalizationX,
    double NewLocalizationY
    ) : IRequest<UpdatePostResponse>;