using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

[Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Reservation))]
public class Reservation : DomainEntityId
{
    public Guid ClientId { get; set; }
    [Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Client))]
    public Client? Client { get; set; }

    public Guid RoomTypeId { get; set; }
    [Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(RoomType))]
    public RoomType? RoomType { get; set; }
    
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Start))]
    public DateTime Start { get; set; } = default!;
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(End))]
    public DateTime End { get; set; } = default!;
}