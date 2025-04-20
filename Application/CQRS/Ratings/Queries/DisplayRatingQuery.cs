using Application.CQRS.Ratings.Dtos;
using MediatR;

namespace Application.CQRS.Ratings.Queries;

public record DisplayRatingQuery(
    Guid PlaceId
    ):IRequest<RatingDto>;