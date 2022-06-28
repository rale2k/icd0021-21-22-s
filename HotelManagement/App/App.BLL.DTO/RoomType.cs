using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.BLL.DTO;

[Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(RoomType))]
public class RoomType : DomainEntityId
{
    public Guid HotelId { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Hotel))]
    public Hotel Hotel { get; set; } = default!;
    
    [Column(TypeName = "json")]
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Name))]
    public string Name { get; set; } = default!;
    
    [Column(TypeName = "json")]
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Description))]
    public string Description { get; set; } = default!;
    
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Capacity))]
    public int Capacity { get; set; } 
    
    public ICollection<Amenity>? Amenities { get; set; }
}