using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class TicketService : BaseEntityService<App.BLL.DTO.Ticket, App.DAL.DTO.Ticket, ITicketRepository>, ITicketService
{
    public TicketService(ITicketRepository repo, IMapper<App.BLL.DTO.Ticket, App.DAL.DTO.Ticket> mapper) : base(repo, mapper)
    {
    }

    public Ticket Add(Ticket entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        return base.Add(entity);
    }

    public new Ticket Update(Ticket entity)
    {
        entity.UpdatedAt = DateTime.UtcNow;
        return base.Update(entity);
    }

    public IEnumerable<Ticket?> GetAllHotelTickets(Guid hotelId, bool noTracking = true)
    {
        return Repository.GetAllHotelTickets(hotelId, noTracking)
            .Select(e => Mapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<Ticket?>> GetAllHotelTicketsAsync(Guid hotelId, bool noTracking = true)
    {
        return (await Repository.GetAllHotelTicketsAsync(hotelId, noTracking))
            .Select(e => Mapper.Map(e))
            .ToList();
    }

    public IEnumerable<Ticket?> GetAllRoomTickets(Guid roomId, bool noTracking = true)
    {
        return Repository.GetAllRoomTickets(roomId, noTracking)
            .Select(e => Mapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<Ticket?>> GetAllRoomTicketsAsync(Guid roomId, bool noTracking = true)
    {
        return (await Repository.GetAllRoomTicketsAsync(roomId, noTracking))
            .Select(e => Mapper.Map(e))
            .ToList();
    }
}