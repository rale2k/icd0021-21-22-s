using Base.Domain;

namespace App.DAL.DTO;
public class Amenity : DomainEntityId
{
    public Guid HotelId { get; set; }
    public Hotel? Hotel { get; set; }

    public string Name { get; set; } = default!;
    
    public string Description { get; set; } = default!;
}

