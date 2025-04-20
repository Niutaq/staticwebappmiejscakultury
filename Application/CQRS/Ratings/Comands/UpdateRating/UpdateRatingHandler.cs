using Application.CQRS.Ratings.Responses;
using Application.Persistance.Interfaces.AccountInterfaces;
using Application.Persistance.Interfaces.PostsInterfaces;
using MediatR;

namespace Application.CQRS.Ratings.Comands.UpdateRating;

public class UpdateRatingHandler:IRequestHandler<UpdateRatingCommand,UpdateRatingResponse>
{
    private readonly IPostsRepository _postsRepository;
    private readonly ICurrentUserService _currentUser;

    public UpdateRatingHandler(IPostsRepository postsRepository,ICurrentUserService currentUser)
    {
        _postsRepository = postsRepository;
        _currentUser = currentUser;
    }

    public async  Task<UpdateRatingResponse> Handle(UpdateRatingCommand request, CancellationToken cancellationToken)
    {
        await _postsRepository.IsPostExistAsync(request.PlaceId, cancellationToken);
        var userId = _currentUser.UserId;
        await _postsRepository.UpdateRatingAsync(userId, request.PlaceId, request.NewRating,
            cancellationToken);
        return new UpdateRatingResponse("Ocena zaktualizowana!");
    }
}