using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using Base.Domain;

namespace App.Public.DTO;

public class Ticket : DomainEntityId
{
    public Guid RoomId { get; set; }
    public Guid HotelId { get; set; }

    public EPriority Priority { get; set; } = default!;
    public ETicketType Type { get; set; } = default!;
    public ETicketStatus Status { get; set; } = default!;

    [MaxLength(1024)]
    public string Description { get; set; } = default!;

    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}