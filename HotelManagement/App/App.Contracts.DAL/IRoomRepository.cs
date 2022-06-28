using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IRoomRepository : IEntityRepository<Room>
{
    bool IsHotelUserRoom(Guid roomId, Guid userId);
    IEnumerable<Room?> GetHotelRooms(Guid hotelId, bool noTracking = true);
    Task<IEnumerable<Room?>> GetHotelRoomsAsync(Guid hotelId, bool noTracking = true);
    IEnumerable<Room?> GetSectionRooms(Guid sectionId, bool noTracking = true);
    Task<IEnumerable<Room?>> GetSectionRoomsAsync(Guid sectionId, bool noTracking = true);
}