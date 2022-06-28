using AutoMapper;
using Base.DAL;

namespace App.DAL.Mappers;

public class ReservationMapper : BaseMapper<App.DAL.DTO.Reservation, App.Domain.Reservation>
{
    public ReservationMapper(IMapper mapper) : base(mapper)
    {
    }
}