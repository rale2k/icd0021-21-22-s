using App.Domain.Enums;
using Base.Domain;

namespace App.DAL.DTO;

public class Room : DomainEntityId
{
    public string Name { get; set; } = default!;
    
    public Guid RoomTypeId { get; set; }
    public RoomType? RoomType { get; set; }
    
    public Guid SectionId { get; set; }
    public Section? Section  { get; set; }
    
    public ERoomStatus Status { get; set; }
}