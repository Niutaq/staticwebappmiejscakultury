using Application.CQRS.Ratings.Responses;
using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.PostsInterfaces;
using MediatR;

namespace Application.CQRS.Ratings.Comands.AddRating;

public class AddRatingHandler:IRequestHandler<AddRatingCommand,AddRatingResponse>
{
    private readonly IPostsRepository _postsRepository;
    private readonly ICurrentUserService _currentUser;

    public AddRatingHandler(IPostsRepository postsRepository,ICurrentUserService currentUser)
    {
        _postsRepository = postsRepository;
        _currentUser = currentUser;
    }
    
    public async Task<AddRatingResponse> Handle(AddRatingCommand request, CancellationToken cancellationToken)
    {
        await _postsRepository.IsPostExistAsync(request.PlaceId, cancellationToken);
        var userId = _currentUser.UserId;
        await _postsRepository.IsRatingExistAsync(request.PlaceId, userId, cancellationToken);
        var newRating = new Domain.Entities.Ratings(request.Rating, request.PlaceId, userId);
        await _postsRepository.AddRatingAsync(newRating, cancellationToken);
        await _postsRepository.UpdateAverageRatingsAsync(request.PlaceId, cancellationToken);
        return new AddRatingResponse("Dodano ocenę");
    }
}