using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IRoomTypeRepository : IEntityRepository<RoomType>
{
    IEnumerable<RoomType?> GetHotelRoomTypes(Guid hotelId, bool noTracking = true);
    Task<IEnumerable<RoomType?>> GetHotelRoomTypesAsync(Guid hotelId, bool noTracking = true);
}