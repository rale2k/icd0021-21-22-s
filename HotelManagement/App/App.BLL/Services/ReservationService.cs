using App.BLL.DTO;
using App.Contracts.BLL.Services;
using App.Contracts.DAL;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class ReservationService : BaseEntityService<App.BLL.DTO.Reservation, App.DAL.DTO.Reservation, IReservationRepository>, IReservationService
{
    public ReservationService(IReservationRepository repo, IMapper<App.BLL.DTO.Reservation, App.DAL.DTO.Reservation> mapper) : base(repo, mapper)
    {
    }

    public bool IsHotelUserReservation(Guid reservationId, Guid userId)
    {
        return Repository.IsHotelUserReservation(reservationId, userId);
    }

    public IEnumerable<Reservation?> GetHotelReservations(Guid hotelId, bool noTracking = true)
    {
        return Repository.GetHotelReservations(hotelId, noTracking)
            .Select(e => Mapper.Map(e))
            .ToList();
    }

    public async Task<IEnumerable<Reservation?>> GetHotelReservationsAsync(Guid hotelId, bool noTracking = true)
    {
        return (await Repository.GetHotelReservationsAsync(hotelId, noTracking))
            .Select(e => Mapper.Map(e))
            .ToList();
    }
}