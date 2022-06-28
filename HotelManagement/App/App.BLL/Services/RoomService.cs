using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RoomService : BaseEntityService<App.BLL.DTO.Room, App.DAL.DTO.Room, IRoomRepository>, IRoomService
{
    public RoomService(IRoomRepository repo, IMapper<App.BLL.DTO.Room, App.DAL.DTO.Room> mapper) : base(repo, mapper)
    {
    }

    public bool IsHotelUserRoom(Guid roomId, Guid userId)
    {
        return Repository.IsHotelUserRoom(roomId, userId);
    }

    public IEnumerable<Room?> GetHotelRooms(Guid hotelId, bool noTracking = true)
    {
        return Repository.GetHotelRooms(hotelId, noTracking)
            .Select(e => Mapper.Map(e));
    }

    public async Task<IEnumerable<Room?>> GetHotelRoomsAsync(Guid hotelId, bool noTracking = true)
    {
        return (await Repository.GetHotelRoomsAsync(hotelId, noTracking))
            .Select(e => Mapper.Map(e));
    }

    public IEnumerable<Room?> GetSectionRooms(Guid sectionId, bool noTracking = true)
    {
        return Repository.GetSectionRooms(sectionId, noTracking)
            .Select(e => Mapper.Map(e));
    }

    public async Task<IEnumerable<Room?>> GetSectionRoomsAsync(Guid sectionId, bool noTracking = true)
    {
        return (await Repository.GetSectionRoomsAsync(sectionId, noTracking))
            .Select(e => Mapper.Map(e));
    }
}