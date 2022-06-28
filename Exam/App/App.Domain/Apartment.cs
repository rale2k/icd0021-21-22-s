using Base.Domain;

namespace App.Domain;

public class Apartment : DomainEntityId
{
    public Guid BuildingId { get; set; }
    public Building? Building { get; set; }
    
    public string Description { get; set; } = default!;
    public int RoomCount { get; set; } = default!;
    public decimal SurfaceArea { get; set; } = default!;

    public ICollection<Amenity>? Amenities { get; set; }
}