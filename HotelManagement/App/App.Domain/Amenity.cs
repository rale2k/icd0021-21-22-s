using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;
[Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Amenity))]
public class Amenity : DomainEntityId
{
    public Guid HotelId { get; set; }
    [Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Hotel))]
    public Hotel? Hotel { get; set; }

    [Column(TypeName = "json")]
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Name))]
    public LangStr Name { get; set; } = new();
    
    [Column(TypeName = "json")]
    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Description))]
    public LangStr Description { get; set; } = new();
    
    public ICollection<RoomType>? RoomTypes { get; set; }
}

