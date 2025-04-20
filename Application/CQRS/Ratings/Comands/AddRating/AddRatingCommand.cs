using Application.CQRS.Ratings.Responses;
using Domain.Enums;
using MediatR;

namespace Application.CQRS.Ratings.Comands.AddRating;

public record AddRatingCommand(
    Guid PlaceId,
    TypesOfRatings Rating
    ):IRequest<AddRatingResponse>;