using Application.CQRS.Posts.Dtos;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Posts.Queries.DisplayPosts;

public record DisplayPostsQuery(
    PlacesCategory Category
    ) : IRequest<List<DisplayPostsDto>>;