using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using Base.Domain;

namespace App.Domain;

[Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Room))]

public class Room : DomainEntityId
{
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Name))]
    public string Name { get; set; } = default!;
    
    public Guid RoomTypeId { get; set; }
    [Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(RoomType))]
    public RoomType? RoomType { get; set; }
    
    public Guid SectionId { get; set; }
    [Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Section))]
    public Section? Section  { get; set; }
    
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Status))]
    public ERoomStatus Status { get; set; }
    
    public ICollection<Ticket>? Tickets { get; set; }
    public ICollection<Stay>? Stays { get; set; }
}