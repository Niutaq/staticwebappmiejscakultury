using Application.CQRS.Ratings.Dtos;

namespace Application.CQRS.Ratings.Extension;

public static class Extension
{
    public static RatingDto RatingAsDto(this Domain.Entities.Ratings rating)
    {
        
        return new RatingDto
        {
            Id = rating.Id,
            Rating = rating.Rating,
            PlaceId = rating.PlacesId
        };
    }
}