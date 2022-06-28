using App.BLL.DTO;
using Base.Contracts.BLL;

namespace App.Contracts.BLL.Services;

public interface IReservationService : IEntityService<Reservation>
{
    bool IsHotelUserReservation(Guid reservationId, Guid userId);
    IEnumerable<Reservation?> GetHotelReservations(Guid hotelId, bool noTracking = true);
    Task<IEnumerable<Reservation?>> GetHotelReservationsAsync(Guid hotelId, bool noTracking = true);
}