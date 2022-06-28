using Base.Domain;

namespace App.BLL.DTO;

public class RoomAmenity : DomainEntityId
{
    public Guid RoomId { get; set; }
    
    public Guid AmenityId { get; set; }
}