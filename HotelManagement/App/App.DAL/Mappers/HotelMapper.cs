using AutoMapper;
using Base.DAL;

namespace App.DAL.Mappers;

public class HotelMapper : BaseMapper<App.DAL.DTO.Hotel, App.Domain.Hotel>
{
    public HotelMapper(IMapper mapper) : base(mapper)
    {
    }
}