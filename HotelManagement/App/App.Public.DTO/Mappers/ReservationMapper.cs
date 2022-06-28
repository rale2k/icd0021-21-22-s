using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class ReservationMapper : BaseMapper<App.Public.DTO.Reservation, App.BLL.DTO.Reservation>
{
    public ReservationMapper(IMapper mapper) : base(mapper)
    {
    }
}