using Base.Domain;

namespace App.Public.DTO;

public class Apartment : DomainEntityId
{
    public Guid BuildingId { get; set; }
    public Guid? ContractId { get; set; }

    public string Description { get; set; } = default!;
    public int RoomCount { get; set; } = default!;
    public decimal SurfaceArea { get; set; } = default!;

    public ICollection<Amenity>? Amenities { get; set; }
}