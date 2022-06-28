using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class UserHotelMapper : BaseMapper<App.Public.DTO.UserHotel, App.BLL.DTO.UserHotel>
{
    public UserHotelMapper(IMapper mapper) : base(mapper)
    {
    }
}