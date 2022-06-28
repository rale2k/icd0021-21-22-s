using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Public.DTO;

public class RoomType : DomainEntityId
{
    public Guid HotelId { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    [MinLength(1)]
    [MaxLength(1024)]
    public string Description { get; set; } = default!;

    [Range(1, int.MaxValue)]
    public int Capacity { get; set; }
    
    public ICollection<Amenity>? Amenities { get; set; }
}