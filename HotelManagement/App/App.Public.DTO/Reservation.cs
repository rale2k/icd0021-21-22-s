using Base.Domain;

namespace App.Public.DTO;

public class Reservation : DomainEntityId
{
    public Client? Client { get; set; }
    
    public Guid? RoomTypeId { get; set; }
    
    public DateTime Start { get; set; } = default!;
    public DateTime End { get; set; } = default!;
}