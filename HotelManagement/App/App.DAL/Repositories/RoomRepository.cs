using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class RoomRepository : BaseEntityRepository<App.DAL.DTO.Room, Domain.Room, AppDbContext>, IRoomRepository
{
    public RoomRepository(AppDbContext dbContext, IMapper<DTO.Room, Domain.Room> mapper) : base(dbContext, mapper)
    {
    }
    
    public bool IsHotelUserRoom(Guid roomId, Guid userId)
    {
        var room = CreateQuery()
            .Include(e => e.Section)
            .FirstOrDefault(e => e.Id == roomId);
        
        if (room != null)
        {
            return RepoDbContext.UserHotels.Any(e => e.HotelId == room.Section!.HotelId && e.UserId == userId);
        }

        return false;
    }

    public IEnumerable<Room?> GetHotelRooms(Guid hotelId, bool noTracking = true)
    {
        return CreateQuery(noTracking)
            .Include(e => e.Section)
            .Where(e => e.Section!.HotelId == hotelId)
            .Select(e => RepoMapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<Room?>> GetHotelRoomsAsync(Guid hotelId, bool noTracking = true)
    {
        return await CreateQuery(noTracking)
            .Include(e => e.Section)
            .Where(e => e.Section!.HotelId == hotelId)
            .Select(e => RepoMapper.Map(e))
            .ToListAsync();
    }

    public IEnumerable<Room?> GetSectionRooms(Guid sectionId, bool noTracking = true)
    {
        return CreateQuery(noTracking).Where(e => e.SectionId == sectionId)
            .Select(e => RepoMapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<Room?>> GetSectionRoomsAsync(Guid sectionId, bool noTracking = true)
    {
        return await CreateQuery(noTracking).Where(e => e.SectionId == sectionId)
            .Select(e => RepoMapper.Map(e))
            .ToListAsync();
    }
}