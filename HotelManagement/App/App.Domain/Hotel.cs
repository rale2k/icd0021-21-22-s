using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

[Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Hotel))]
public class Hotel : DomainEntityId
{    
    [Column(TypeName = "json")]
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Name))]
    public LangStr Name { get; set; } = new();
    
    [Column(TypeName = "json")]
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Description))]
    public LangStr Description { get; set; } = new();
    
    public ICollection<Ticket>? Tickets { get; set; } 
    public ICollection<Section>? HotelSections { get; set; }
    public ICollection<Amenity>? Amenities { get; set; }
    public ICollection<RoomType>? RoomTypes { get; set; }
}