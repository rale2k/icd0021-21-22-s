using Base.Domain;

namespace App.DAL.DTO;

public class Stay : DomainEntityId
{
    public Guid ClientId { get; set; }
    public Client? Client { get; set; }

    public Guid RoomId { get; set; }
    public Room? Room { get; set; }
    
    public DateTime Start { get; set; } = default!;
    public DateTime End { get; set; } = default!;
}