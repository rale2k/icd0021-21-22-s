using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class HotelMapper : BaseMapper<App.BLL.DTO.Hotel, App.DAL.DTO.Hotel>
{
    public HotelMapper(IMapper mapper) : base(mapper)
    {
    }
}