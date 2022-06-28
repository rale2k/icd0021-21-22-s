using App.Contracts.BLL.Services;
using Base.Contracts.BLL;

namespace App.Contracts.BLL;

public interface IAppBll : IBll
{
    IAmenityService Amenities { get; }
    IClientService Clients { get; }
    IHotelService Hotels { get; }
    IUserHotelService UserHotels { get; }
    IReservationService Reservations { get; }
    IRoomService Rooms { get; }
    IRoomTypeService RoomTypes { get; }
    ISectionService Sections { get; }
    IStayService Stays { get; }
    ITicketService Tickets { get; }
}