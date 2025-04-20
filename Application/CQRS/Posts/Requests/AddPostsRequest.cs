using Domain.Enums;

namespace Application.CQRS.Posts.Requests;

public record AddPostsRequest(
    string Title,
    string Description,
    PlacesCategory Category,
    double LocalizationX,
    double LocalizationY
    );