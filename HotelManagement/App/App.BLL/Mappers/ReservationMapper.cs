using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ReservationMapper : BaseMapper<App.BLL.DTO.Reservation, App.DAL.DTO.Reservation>
{
    public ReservationMapper(IMapper mapper) : base(mapper)
    {
    }
}