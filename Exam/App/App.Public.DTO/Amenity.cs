using Base.Domain;

namespace App.Public.DTO;

public class Amenity : DomainEntityId
{
    public string Name { get; set; } = default!;
    
}