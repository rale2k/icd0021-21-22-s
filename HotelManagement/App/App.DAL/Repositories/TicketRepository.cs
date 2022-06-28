using App.Contracts.DAL;
using App.DAL.DTO;
using Base.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.Repositories;

public class TicketRepository : BaseEntityRepository<App.DAL.DTO.Ticket, Domain.Ticket, AppDbContext>, ITicketRepository
{
    public TicketRepository(AppDbContext dbContext, IMapper<DTO.Ticket, Domain.Ticket> mapper) : base(dbContext, mapper)
    {
    }

    public override Ticket Update(Ticket entity)
    {
        var domainEnt = RepoMapper.Map(entity)!;

        RepoDbContext.Attach(domainEnt);
        RepoDbContext.Entry(domainEnt).State = EntityState.Modified;
        RepoDbContext.Entry(domainEnt).Property(e => e.CreatedAt).IsModified = false;

        return RepoMapper.Map(domainEnt)!;
    }
    
    public IEnumerable<Ticket?> GetAllHotelTickets(Guid hotelId, bool noTracking = true)
    {
        return CreateQuery(noTracking).Where(e => e.HotelId == hotelId)
            .Select(e => RepoMapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<Ticket?>> GetAllHotelTicketsAsync(Guid hotelId, bool noTracking = true)
    {
        return await CreateQuery(noTracking).Where(e => e.HotelId == hotelId)
            .Select(e => RepoMapper.Map(e))
            .ToListAsync();
    }

    public IEnumerable<Ticket?> GetAllRoomTickets(Guid roomId, bool noTracking = true)
    {
        return CreateQuery(noTracking).Where(e => e.RoomId == roomId)
            .Select(e => RepoMapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<Ticket?>> GetAllRoomTicketsAsync(Guid roomId, bool noTracking = true)
    {
        return await CreateQuery(noTracking).Where(e => e.RoomId == roomId)
            .Select(e => RepoMapper.Map(e))
            .ToListAsync();
    }
}