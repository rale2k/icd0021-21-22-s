using System.ComponentModel.DataAnnotations;
using App.Domain.Enums;
using Base.Domain;

namespace App.Public.DTO;

public class Room : DomainEntityId
{
    [MinLength(1)]
    [MaxLength(128)]
    public string Name { get; set; } = default!;
    
    public Guid RoomTypeId { get; set; }
    
    public Guid SectionId { get; set; }
    
    public ERoomStatus Status { get; set; }
}