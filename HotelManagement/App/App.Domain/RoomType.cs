using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

[Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(RoomType))]
public class RoomType : DomainEntityId
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

    [Display(ResourceType = typeof(Resources.Domain.Entity.Property), Name = nameof(Capacity))]
    public int Capacity { get; set; }

    public ICollection<Amenity>? Amenities { get; set; }
    public ICollection<Reservation>? Reservations { get; set; }
    public ICollection<Room>? Rooms { get; set; }
}