using Base.Domain;

namespace App.Domain;

public class Amenity : DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public ICollection<Apartment>? Apartments { get; set; }
}