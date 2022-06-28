using App.Domain.Enums;
using Base.Domain;

namespace App.DAL.DTO;

public class Ticket : DomainEntityMetaId
{
    public Guid RoomId { get; set; }
    public Room? Room { get; set; }
    
    public Guid HotelId { get; set; }
    public Hotel? Hotel { get; set; }

    public EPriority Priority { get; set; } = default!;
    public ETicketType Type { get; set; } = default!;
    public ETicketStatus Status { get; set; } = default!;

    public string Description { get; set; } = default!;

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}