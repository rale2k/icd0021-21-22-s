using Base.Domain;

namespace App.BLL.DTO;

public class Reservation : DomainEntityId
{
    public Guid? ClientId { get; set; }
    public Client? Client { get; set; }
    
    public Guid? RoomTypeId { get; set; }
    public RoomType? RoomType { get; set; }
    
    public DateTime Start { get; set; } = default!;
    public DateTime End { get; set; } = default!;
}