using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class ReservationRepository : BaseEntityRepository<App.DAL.DTO.Reservation, Domain.Reservation, AppDbContext>, IReservationRepository
{
    public ReservationRepository(AppDbContext dbContext, IMapper<DTO.Reservation, Domain.Reservation> mapper) : base(dbContext, mapper)
    {
    }

    protected override IQueryable<Domain.Reservation> CreateQuery(bool noTracking = true)
    {
        return base.CreateQuery(noTracking).Include(e => e.Client);
    }

    public bool IsHotelUserReservation(Guid reservationId, Guid userId)
    {
        var res = CreateQuery()
            .Include(e => e.RoomType)
            .FirstOrDefault(e => e.Id == reservationId);
        
        if (res != null)
        {
            return RepoDbContext.UserHotels.Any(e => e.HotelId == res.RoomType!.HotelId && e.UserId == userId);
        }

        return false;
    }

    public IEnumerable<Reservation?> GetHotelReservations(Guid hotelId, bool noTracking = true)
    {
        return CreateQuery(noTracking)
            .Include(e => e.RoomType)
            .Include(e => e.Client)
            .Where(e => e.RoomType!.HotelId == hotelId)
            .Select(e => RepoMapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<Reservation?>> GetHotelReservationsAsync(Guid hotelId, bool noTracking = true)
    {
        var res = (await CreateQuery(noTracking)
            .Include(e => e.RoomType)
            .Include(e => e.Client)
            .Where(e => e.RoomType!.HotelId == hotelId)
            .Select(e => RepoMapper.Map(e))
            .ToListAsync());
        
        return res;
    }
}