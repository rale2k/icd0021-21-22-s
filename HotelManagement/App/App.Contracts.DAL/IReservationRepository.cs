using App.DAL.DTO;
using Base.Contracts.DAL;

namespace App.Contracts.DAL;

public interface IReservationRepository : IEntityRepository<Reservation>
{
    bool IsHotelUserReservation(Guid reservationId, Guid userId);
    IEnumerable<Reservation?> GetHotelReservations(Guid hotelId, bool noTracking = true);
    Task<IEnumerable<Reservation?>> GetHotelReservationsAsync(Guid hotelId, bool noTracking = true);
}