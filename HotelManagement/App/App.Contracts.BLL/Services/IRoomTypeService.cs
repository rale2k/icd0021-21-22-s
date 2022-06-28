using App.BLL.DTO;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IRoomTypeService : IEntityService<RoomType>
{
    IEnumerable<RoomType?> GetHotelRoomTypes(Guid hotelId, bool noTracking = true);
    Task<IEnumerable<RoomType?>> GetHotelRoomTypesAsync(Guid hotelId, bool noTracking = true);
}