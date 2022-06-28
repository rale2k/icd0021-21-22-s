using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class HotelMapper : BaseMapper<App.Public.DTO.Hotel, App.BLL.DTO.Hotel>
{
    public HotelMapper(IMapper mapper) : base(mapper)
    {
    }
}