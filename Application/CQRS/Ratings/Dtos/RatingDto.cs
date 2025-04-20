using Domain.Enums;

namespace Application.CQRS.Ratings.Dtos;

public class RatingDto
{
    public Guid Id { get; set; }
    public TypesOfRatings Rating { get; set; }
    public Guid PlaceId { get; set; }
}