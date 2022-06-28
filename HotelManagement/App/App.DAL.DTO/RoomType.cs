using Base.Domain;

namespace App.DAL.DTO;

public class RoomType : DomainEntityId
{
    public Guid HotelId { get; set; }
    
    public Hotel Hotel { get; set; } = default!;
    public string Name { get; set; } = default!;
    
    public string Description { get; set; } = default!;
    
    public int Capacity { get; set; }
    
    public ICollection<Amenity>? Amenities { get; set; }
}