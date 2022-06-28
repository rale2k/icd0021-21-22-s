using Base.Domain;

namespace App.Public.DTO;

public class Stay : DomainEntityId
{
    public Guid RoomId { get; set; }
    public Client Client { get; set; } = default!;
    
    public DateTime Start { get; set; } = default!;
    public DateTime End { get; set; } = default!;
}