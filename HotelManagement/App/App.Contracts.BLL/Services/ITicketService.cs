using App.BLL.DTO;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface ITicketService : IEntityService<Ticket>
{
    IEnumerable<Ticket?> GetAllHotelTickets(Guid hotelId, bool noTracking = true);
    Task<IEnumerable<Ticket?>> GetAllHotelTicketsAsync(Guid hotelId, bool noTracking = true);
    IEnumerable<Ticket?> GetAllRoomTickets(Guid roomId, bool noTracking = true);
    Task<IEnumerable<Ticket?>> GetAllRoomTicketsAsync(Guid roomId, bool noTracking = true);

}