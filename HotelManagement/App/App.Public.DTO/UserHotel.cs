using Base.Domain;

namespace App.Public.DTO;

public class UserHotel : DomainEntityId
{
    public Guid UserId { get; set; }
    public Guid HotelId { get; set; }
}