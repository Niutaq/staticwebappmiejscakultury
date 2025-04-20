using Application.CQRS.Ratings.Dtos;
using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.PostsInterfaces;
using MediatR;

namespace Application.CQRS.Ratings.Queries;

public class DisplayRatingHandler:IRequestHandler<DisplayRatingQuery,RatingDto>
{
    private readonly IPostsRepository _postsRepository;
    private readonly ICurrentUserService _currentUserService;

    public DisplayRatingHandler(IPostsRepository postsRepository,ICurrentUserService currentUserService)
    {
        _postsRepository = postsRepository;
        _currentUserService = currentUserService;
    }
    public Task<RatingDto> Handle(DisplayRatingQuery request, CancellationToken cancellationToken)
    {
        var userId = _currentUserService.UserId;
        return _postsRepository.DisplayRatingAsync(request.PlaceId, userId, cancellationToken);
    }
}