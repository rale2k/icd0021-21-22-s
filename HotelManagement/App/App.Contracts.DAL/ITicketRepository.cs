using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface ITicketRepository : IEntityRepository<Ticket>
{
    IEnumerable<Ticket?> GetAllHotelTickets(Guid hotelId, bool noTracking = true);
    Task<IEnumerable<Ticket?>> GetAllHotelTicketsAsync(Guid hotelId, bool noTracking = true);
    IEnumerable<Ticket?> GetAllRoomTickets(Guid roomId, bool noTracking = true);
    Task<IEnumerable<Ticket?>> GetAllRoomTicketsAsync(Guid roomId, bool noTracking = true);
}