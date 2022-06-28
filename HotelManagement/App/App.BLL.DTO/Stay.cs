using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.BLL.DTO;

[Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Stay))]
public class Stay : DomainEntityId
{
    public Guid ClientId { get; set; }
    [Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Client))]
    public Client? Client { get; set; }

    public Guid RoomId { get; set; }
    [Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Room))]
    public Room? Room { get; set; }
    
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Start))]
    public DateTime Start { get; set; } = default!;
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(End))]
    public DateTime End { get; set; } = default!;
}