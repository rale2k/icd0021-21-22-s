using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IAppUnitOfWork : IUnitOfWork
{
    IAmenityRepository Amenities { get; }
    IClientRepository Clients { get; }
    IHotelRepository Hotels { get; }
    IUserHotelRepository UserHotels { get; }
    IReservationRepository Reservations { get; }
    IRoomRepository Rooms { get; }
    IRoomTypeRepository RoomTypes { get; }
    ISectionRepository Sections { get; }
    IStayRepository Stays { get; }
    ITicketRepository Tickets { get; }

}