using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class StayRepository : BaseEntityRepository<App.DAL.DTO.Stay, Domain.Stay, AppDbContext>, IStayRepository
{
    public StayRepository(AppDbContext dbContext, IMapper<DTO.Stay, Domain.Stay> mapper) : base(dbContext, mapper)
    {
    }

    protected override IQueryable<Domain.Stay> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking).Include(e => e.Client);
    }

    public bool IsHotelUserStay(Guid stayId, Guid userId)
    {
        var stay = CreateQuery()
            .Include(e => e.Room)
            .Include(e => e.Room!.Section)
            .FirstOrDefault(e => e.Id == stayId) ;
        
        if (stay != null)
        {
            return RepoDbContext.UserHotels.Any(e => e.HotelId == stay.Room!.Section!.HotelId && e.UserId == userId);
        }

        return false;
    }

    public IEnumerable<Stay?> GetAllRoomStays(Guid roomId, bool noTracking = true)
    {
        return CreateQuery(noTracking).Where(e => e.RoomId == roomId)
            .Select(e => RepoMapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<Stay?>> GetAllRoomStaysAsync(Guid roomId, bool noTracking = true)
    {
        return await CreateQuery(noTracking).Where(e => e.RoomId == roomId)
            .Select(e => RepoMapper.Map(e))
            .ToListAsync();
    }
}