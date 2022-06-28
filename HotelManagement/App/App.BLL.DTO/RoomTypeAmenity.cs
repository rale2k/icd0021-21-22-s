using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.BLL.DTO;

[Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(RoomTypeAmenity))]
public class RoomTypeAmenity : DomainEntityId
{
    public Guid RoomTypeId { get; set; }
    [Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(RoomType))]
    public RoomType? RoomType { get; set; }
    
    public Guid AmenityId { get; set; }
    [Display(ResourceType = typeof(Resources.Domain.Entity.Name), Name = nameof(Amenity))]
    public Amenity? Amenity { get; set; }
}