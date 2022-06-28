using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.BLL.DTO;

[Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Section))]
public class Section : DomainEntityId
{
    public Guid HotelId { get; set; }
    [Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Hotel))]
    public Hotel? Hotel { get; set; }
    
    [MaxLength(128)]
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Name))]
    public string Name { get; set; } = default!;
    
    [MaxLength(1024)]
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Description))]
    public string Description { get; set; } = default!;
    
    public ICollection<Room>? Rooms { get; set; }
}