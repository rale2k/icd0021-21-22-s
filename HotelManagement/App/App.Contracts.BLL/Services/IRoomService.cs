using App.BLL.DTO;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IRoomService : IEntityService<Room>
{
    public bool IsHotelUserRoom(Guid roomId, Guid userId);
    IEnumerable<Room?> GetHotelRooms(Guid hotelId, bool noTracking = true);
    Task<IEnumerable<Room?>> GetHotelRoomsAsync(Guid hotelId, bool noTracking = true);

    IEnumerable<Room?> GetSectionRooms(Guid sectionId, bool noTracking = true);
    Task<IEnumerable<Room?>> GetSectionRoomsAsync(Guid sectionId, bool noTracking = true);
}