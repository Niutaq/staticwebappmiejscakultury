using Domain.Enums;

namespace Domain.Entities;

public class Ratings
{

    public Guid Id { get; set; }
    public TypesOfRatings Rating { get; set; }
    
    
    public Guid PlacesId { get; set; }
    public Guid UsersId { get; set; }
    public Places Places { get; set; }
    public Users Users { get; set; }

    public Ratings()
    {
        
    }

    public Ratings(TypesOfRatings rating,Guid placesId,Guid usersId )
    {
        Rating = rating;
        PlacesId = placesId;
        UsersId = usersId;
    }
}
