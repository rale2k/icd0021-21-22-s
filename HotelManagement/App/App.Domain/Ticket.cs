using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using Base.Domain;

namespace App.Domain;

[Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Ticket))]
public class Ticket : DomainEntityMetaId
{
    public Guid RoomId { get; set; }
    [Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Room))]
    public Room? Room { get; set; }
    
    public Guid HotelId { get; set; }
    [Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Ticket))]
    public Hotel? Hotel { get; set; }

    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Priority))]
    public EPriority Priority { get; set; } = default!;
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Type))]
    public ETicketType Type { get; set; } = default!;
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Status))]
    public ETicketStatus Status { get; set; } = default!;

    [MaxLength(1024)]
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Description))]
    public string Description { get; set; } = default!;
}