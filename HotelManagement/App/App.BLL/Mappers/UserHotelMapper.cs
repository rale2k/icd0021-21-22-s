using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserHotelMapper : BaseMapper<App.BLL.DTO.UserHotel, App.DAL.DTO.UserHotel>
{
    public UserHotelMapper(IMapper mapper) : base(mapper)
    {
    }
}